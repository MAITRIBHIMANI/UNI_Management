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
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
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
                              BusinessNumber = project.BusinessNumber,
                              Document = ProjectAttachment.Document,
                              ArrivalDate = project.ArrivalDate,
                              CommitmentDate = project.CommitmentDate,
                              IsActive = project.IsActive,
                          }).ToList();

            return result;
        }

        public IEnumerable<ProjectDetailsViewModel> GetProjectListfilter(string filterprojectname, string filterbusinessnumber, DateTime? filterarrivaldate)
        {
           
            IEnumerable<ProjectDetailsViewModel> projectList = (from project in _context.Projects
                               join projectWiseAttachment in _context.ProjectAttachments
                                on project.ProjectId equals projectWiseAttachment.ProjectId into ProjectGrop
                               from ProjectAttachment in ProjectGrop.DefaultIfEmpty()
                               where (project.IsDeleted == false)  
                                      
                               select new ProjectDetailsViewModel
                               {
                                   ProjectId = project.ProjectId,
                                   Name = project.Name,
                                   BusinessNumber = project.BusinessNumber,
                                   Document = ProjectAttachment.Document,
                                   ArrivalDate = project.ArrivalDate,
                                   CommitmentDate = project.CommitmentDate,
                                   IsActive = project.IsActive,
                               }).ToList();

            if (!string.IsNullOrEmpty(filterprojectname))
            {
                projectList = projectList.Where(x => x.Name.ToLower().Contains(filterprojectname.ToLower()));
            }
            if (!string.IsNullOrEmpty(filterbusinessnumber))
            {

                projectList = projectList.Where(x => x.BusinessNumber.ToLower().Contains(filterbusinessnumber.ToLower()));
            }

            if (filterarrivaldate.HasValue)
            {
                projectList = projectList.Where(x => x.ArrivalDate == filterarrivaldate.Value);
            }
            return projectList;


        }

        #endregion

        #region ProjectAdd
        /// <summary>
        /// Emplkyuikiuk
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 


        public void AddProject(ProjectDetailsViewModel model)
        {
            //Project


            try
            {
                var Project = new Project();
                Project.ClientId = model.ClientId;
                Project.DomainId = model.DomainId;  
                Project.Name = model.Name;
                Project.ArrivalDate = model.ArrivalDate;
                Project.CommitmentDate = model.CommitmentDate;
                Project.GitRepo = model.GitRepo;
                Project.BusinessNumber = model.BusinessNumber;
                Project.AdditionalInformation = model.AdditionalInformation;
                Project.IsActive = model.IsActive;
                Project.IsDeleted = false;
                Project.Created = DateTime.Now;
                _context.Projects.Add(Project);
                _context.SaveChanges();
                Project.CreatedBy = Project.ProjectId;
                _context.SaveChanges();
          

                //ProjectAttachment
                var projectattachment = new ProjectAttachment();
                projectattachment.ProjectId = Project.ProjectId;
                projectattachment.Document = Helper.Files(model.Projectdocument, Project.ProjectId, "Projct", "Document.pdf");
                projectattachment.DocDescription = model.DocDescription;
                projectattachment.Created = DateTime.Now;
                projectattachment.IsDeleted = false;
                _context.ProjectAttachments.Add(projectattachment);
                _context.SaveChanges();
                projectattachment.CreatedBy = Project.ProjectId;
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
            Project d = await _context.Projects.Where(x => x.ProjectId == id).FirstOrDefaultAsync();
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
                Project project = _context.Projects.Where(x => x.ProjectId == model.ProjectId).FirstOrDefault();

                if (project != null)
                {
                    project.ClientId = model.ClientId;
                    project.DomainId = model.DomainId;
                    project.Name = model.Name;
                    project.ArrivalDate = model.ArrivalDate;
                    project.CommitmentDate = model.CommitmentDate;
                    project.BusinessNumber = model.BusinessNumber;
                    project.GitRepo = model.GitRepo;
                    project.Modified = DateTime.Now;
                    project.ModifiedBy = model.ProjectId;
                    project.AdditionalInformation = model.AdditionalInformation;
                    project.IsActive = model.IsActive;
                    _context.Projects.Update(project);
                    _context.SaveChanges();

                    ProjectAttachment projectAttachment = _context.ProjectAttachments.Where(x => x.ProjectId == model.ProjectId).FirstOrDefault();

                    if (projectAttachment != null)
                    {
                        projectAttachment.ProjectId = project.ProjectId;                       
                        projectAttachment.Document = model.Document;
                        projectAttachment.DocDescription = model.DocDescription;
                        projectAttachment.Modified = DateTime.Now;
                        projectAttachment.ModifiedBy = model.ProjectId;
                        _context.ProjectAttachments.Update(projectAttachment);
                        _context.SaveChanges();
                    }
                    Client client=_context.Clients.Where(x=>x.ClientId == model.ClientId).FirstOrDefault();
                    if (client != null)
                    {
                        client.ClientId=client.ClientId;
                        client.Name=client.Name;
                    }
                    Domain domain = _context.Domains.Where(x=>x.DomainId== model.DomainId).FirstOrDefault();
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
                                                       on prj.ProjectId equals prjattach.ProjectId into ProjectGroup
                                                       from ProjectAttachment in ProjectGroup.DefaultIfEmpty()
                                                     
                                                       join c in _context.Clients
                                                       on prj.ClientId equals c.ClientId into clientGroup
                                                       from c in clientGroup.DefaultIfEmpty()

                                                       join d in _context.Domains
                                                     on prj.DomainId equals d.DomainId into domainGroup
                                                       from d in domainGroup.DefaultIfEmpty()
                                                       where prj.ProjectId == id && prj.ClientId == c.ClientId
                                                       && prj.DomainId ==d.DomainId
                                                       select new ProjectDetailsViewModel()
                                                       {
                                                           ProjectId = prj.ProjectId,                                                           
                                                           ClientId = (int)prj.ClientId,
                                                           ClientName = c.Name,
                                                           DomainId =(int)prj.DomainId,
                                                           DomainName=d.DomainName,
                                                           ArrivalDate = prj.ArrivalDate,
                                                           CommitmentDate = prj.CommitmentDate,
                                                           GitRepo = prj.GitRepo,
                                                           BusinessNumber = prj.BusinessNumber,
                                                           AdditionalInformation = prj.AdditionalInformation,
                                                           DocDescription = ProjectAttachment.DocDescription,
                                                           Document = ProjectAttachment.Document,
                                                           Name = prj.Name,
                                                           IsActive = prj.IsActive,
                                                           ProjectWiseAttachmentId = ProjectAttachment.ProjectAttachmentId,
                                                           

                                                       }).FirstOrDefault();





            return ProjectDetails;
        }
        #endregion




    }
}
