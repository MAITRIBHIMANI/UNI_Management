using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UNIManagement.Entities.DataContext;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.CommanHelper;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Repositories.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region List
        public List<ProjectDetailsViewModel> GetProjectList()
        {
            var result = (from project in _context.Projects
                          join projectWiseAttachment in _context.ProjectAttachments
                           on project.ProjectId equals projectWiseAttachment.ProjectId into ProjectGrop
                          from ProjectAttachment in ProjectGrop.DefaultIfEmpty()
                          where (project.IsDeleted == false)
                          
                          select new ProjectDetailsViewModel
                          {
                              ProjectId = project.ProjectId,
                              Name = project.Name,
                              BusinessName = project.BusinessName,
                              ArrivalDate = project.ArrivalDate,
                              CommitmentDate = project.CommitmentDate,
                              IsActive = project.IsActive,
                              Document = ProjectAttachment.Document,
                          })
                            .ToList();

            return result;
        }

        public List<ProjectDetailsViewModel> GetProjectListfilter(string filterprojectname, string filterbusinessnumber, DateTime? filterarrivaldate, string filterIsActive)
        {
            List<ProjectDetailsViewModel> projectList = (from project in _context.Projects
                                                         join projectWiseAttachment in _context.ProjectAttachments
                                                         on project.ProjectId equals projectWiseAttachment.ProjectId into ProjectGrop
                                                         from ProjectAttachment in ProjectGrop.DefaultIfEmpty()
                                                         where (project.IsDeleted == false
                                                                && (string.IsNullOrEmpty(filterprojectname) || project.Name.Trim().ToLower().Contains(filterprojectname.ToLower()))
                                                                && (string.IsNullOrEmpty(filterbusinessnumber) || project.BusinessName.ToLower().Contains(filterbusinessnumber.ToLower()))
                                                                && (!filterarrivaldate.HasValue || project.ArrivalDate == filterarrivaldate.Value))
                                                                && (string.IsNullOrEmpty(filterIsActive) || project.IsActive.ToLower() == filterIsActive.ToLower())
                                                         select new ProjectDetailsViewModel
                                                         {
                                                             ProjectId = project.ProjectId,
                                                             Name = project.Name,
                                                             BusinessName = project.BusinessName,
                                                             Document = ProjectAttachment.Document,
                                                             ArrivalDate = project.ArrivalDate,
                                                             CommitmentDate = project.CommitmentDate,
                                                             IsActive = project.IsActive,
                                                         }).ToList();
            return projectList;

          
        }

        #endregion

        #region ProjectAdd  

        public void AddProject(ProjectDetailsViewModel model)
        {
            try
            {
                var Project = new Project();
                Project.Name = model.Name;
                Project.ClientId = model.ClientId;
                Project.DomainId = model.DomainId;
                Project.ArrivalDate = model.ArrivalDate;
                Project.CommitmentDate = model.CommitmentDate;
                Project.GitRepo = model.GitRepo;
                Project.BusinessName = model.BusinessName;
                Project.AdditionalInformation = model.AdditionalInformation;
                Project.IsActive = model.IsActive;
                Project.IsDeleted = false;
                Project.Created = DateTime.Now;
                Project.Modified = Project.Created;
                _context.Projects.Add(Project);
                _context.SaveChanges();
                Project.CreatedBy = Project.ProjectId;
                Project.ModifiedBy = Project.ProjectId;
                _context.SaveChanges();

                //ProjectAttachment
                var projectattachment = new ProjectAttachment();
                projectattachment.ProjectId = Project.ProjectId;
                projectattachment.Document = Helper.UploadFile(model.Projectdocument, Project.ProjectId, "Projct", "Document.pdf");
                projectattachment.DocDescription = model.DocDescription;
                projectattachment.Created = DateTime.Now; 
                projectattachment.Modified = projectattachment.Created;
                projectattachment.IsDeleted = false;
                _context.ProjectAttachments.Add(projectattachment);
                _context.SaveChanges();
                projectattachment.CreatedBy = Project.ProjectId;
                projectattachment.ModifiedBy = Project.ProjectId;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete Emplyoee details from database on EmployeeId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteProjectsync(int id)
        {
            Project? d = await _context.Projects
                                       .Where(x => x.ProjectId == id)
                                       .FirstOrDefaultAsync();
            if (d != null)
            {
                d.IsDeleted = true;
                _context.Projects.Update(d);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Update
        public void UpdateProject(ProjectDetailsViewModel model)
        {
            try
            {
                Project? project = _context.Projects
                                           .Where(x => x.ProjectId == model.ProjectId)
                                           .FirstOrDefault();

                if (project != null)
                {
                    project.ClientId = model.ClientId;
                    project.DomainId = model.DomainId;
                    project.Name = model.Name;
                    project.ArrivalDate = model.ArrivalDate;
                    project.CommitmentDate = model.CommitmentDate;
                    project.BusinessName = model.BusinessName;
                    project.GitRepo = model.GitRepo;
                    project.Modified = DateTime.Now;
                    project.ModifiedBy = model.ProjectId;
                    project.AdditionalInformation = model.AdditionalInformation;
                    project.IsActive = model.IsActive;
                    _context.Projects.Update(project);
                    _context.SaveChanges();

                    ProjectAttachment? projectAttachment = _context.ProjectAttachments.Where(x => x.ProjectId == model.ProjectId).FirstOrDefault();

                    if (projectAttachment != null)
                    {
                        projectAttachment.ProjectId = project.ProjectId;
                        projectAttachment.Document = Helper.UploadFile(model.Projectdocument, projectAttachment.ProjectId, "Projct", "Document.pdf");
                        projectAttachment.DocDescription = model.DocDescription;
                        projectAttachment.Modified = DateTime.Now;
                        projectAttachment.ModifiedBy = model.ProjectId;
                        _context.ProjectAttachments.Update(projectAttachment);
                        _context.SaveChanges();
                    }
                    Client? client = _context.Clients
                                             .Where(x => x.ClientId == model.ClientId)
                                             .FirstOrDefault();
                    if (client != null)
                    {
                        client.ClientId = client.ClientId;
                        client.Name = client.Name;
                    }
                    Domain? domain = _context.Domains
                                             .Where(x => x.DomainId == model.DomainId)
                                             .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return;
            }

        }
        public ProjectDetailsViewModel GetProjectDetails(int id)
        {

            ProjectDetailsViewModel? ProjectDetails = (from prj in _context.Projects
                                                       join prjattach in _context.ProjectAttachments
                                                       on id equals prjattach.ProjectId into ProjectGroup
                                                       from prjattach in ProjectGroup.DefaultIfEmpty()
                                                       join c in _context.Clients
                                                       on prj.ClientId equals c.ClientId into clientGroup
                                                       from c in clientGroup.DefaultIfEmpty()
                                                       join d in _context.Domains
                                                       on prj.DomainId equals d.DomainId into domainGroup
                                                       from d in domainGroup.DefaultIfEmpty()
                                                       where prj.ProjectId == id
                                                       select new ProjectDetailsViewModel()
                                                       {
                                                           ProjectId = prj.ProjectId,
                                                           Name = prj.Name,
                                                           ClientId = prj.ClientId,
                                                           ClientName = c.Name,
                                                           DomainId = prj.DomainId,
                                                           DomainName = d.DomainName,
                                                           ArrivalDate = prj.ArrivalDate,
                                                           CommitmentDate = prj.CommitmentDate,
                                                           GitRepo = prj.GitRepo,
                                                           BusinessName = prj.BusinessName,
                                                           AdditionalInformation = prj.AdditionalInformation,
                                                           DocDescription = prjattach.DocDescription,
                                                           Document = prjattach.Document,
                                                           IsActive = prj.IsActive,
                                                           ProjectWiseAttachmentId = prjattach.ProjectAttachmentId,
                                                           IsDeleted = prj.IsDeleted,

                                                       }).FirstOrDefault();
            return ProjectDetails;
        }
        #endregion
    }
}
