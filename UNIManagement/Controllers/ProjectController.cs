using Microsoft.AspNetCore.Mvc;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Controllers
{
    public class ProjectController : BaseController
    {
        #region Constructor
        private readonly IProjectRepository _projectRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IDomainRepository _domainRepository;
        public ProjectController(IProjectRepository projectRepository, IDomainRepository domainRepository, IClientRepository clientRepository)
        {
            _projectRepository = projectRepository;
            _clientRepository = clientRepository;
            _domainRepository = domainRepository;
        }
        #endregion

        #region Project_List
        public IActionResult Index()
        {
            ViewBag.BusinessNameDropDown = _clientRepository.GetClientList();
            ViewBag.ProjectNameDropDown = _projectRepository.GetProjectList();
            return View("ProjectList");
        }
        /// <summary>
        /// Project List and Filtered Project List
        /// </summary>
        /// <returns></returns>
        public IActionResult GetProjectList(string filterprojectname, string filterbusinessnumber, DateTime? filterarrivaldate, string filterIsActive)
        {
            filterprojectname = filterprojectname == null ? "" : filterprojectname;

            List<ProjectDetailsViewModel> list = _projectRepository.GetProjectListfilter(filterprojectname, filterbusinessnumber, filterarrivaldate, filterIsActive);
            return PartialView("_Partial_ProjectList", list);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Project Delete BY ProjectId
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            await _projectRepository.DeleteProjectsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region View
        /// <summary>
        /// Project View Modal pop
        /// </summary>
        /// <returns></returns>
        public IActionResult View(int id)
        {
            var project = _projectRepository.GetProjectDetails(id);
            return PartialView("_ProjectView", project);
        }

        #endregion

        #region Add
        public IActionResult ProjectForm()
        {
            ViewBag.BusinessNameDropDown = _clientRepository.GetClientList();
            ViewBag.ClientDropDown = _clientRepository.GetClientList();
            ViewBag.DomainDropDown = _domainRepository.GetDomainList();
            return View();
        }
        /// <summary>
        /// Get PRoject Details By ProjectId
        /// </summary>
        /// <returns></returns>
        public IActionResult Update(int id)
        {
            ViewBag.ClientDropDown = _clientRepository.GetClientList();
            ViewBag.DomainDropDown = _domainRepository.GetDomainList();
            ViewBag.BusinessNameDropDown = _clientRepository.GetClientList();

            var Project = _projectRepository.GetProjectDetails(id);
            return View("ProjectForm", Project);

        }
        /// <summary>
        /// Project Add Edit Condition 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult AddEdit(ProjectDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ProjectId > 0)
                    _projectRepository.UpdateProject(model);
                else
                    _projectRepository.AddProject(model);
            }
            else
            {
                ViewBag.BusinessNameDropDown = _clientRepository.GetClientList();
                ViewBag.ClientDropDown = _clientRepository.GetClientList();
                ViewBag.DomainDropDown = _domainRepository.GetDomainList();
                return View("ProjectForm");
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}
