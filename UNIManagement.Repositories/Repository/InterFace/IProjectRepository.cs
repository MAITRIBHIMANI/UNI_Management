using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNIManagement.Entities.ViewModel;

namespace UNIManagement.Repositories.Repository.InterFace
{
    public interface IProjectRepository
    {
        List<ProjectDetailsViewModel> GetProjectList();
        Task DeleteProjectsync(int ProjectId);

         void UpdateProject(ProjectDetailsViewModel model);

        void AddProject(ProjectDetailsViewModel model);    
        public ProjectDetailsViewModel GetProjectDetails(int id);
        IEnumerable<ProjectDetailsViewModel> GetProjectListfilter(string filterprojectname, string filterbusinessnumber, DateTime? filterarrivaldate);

        
    }
}
