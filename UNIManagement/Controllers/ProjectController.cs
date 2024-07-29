using Microsoft.AspNetCore.Mvc;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IDomainRepository _domainRepository;
        public ProjectController(IProjectRepository projectRepository, IDomainRepository domainRepository , IClientRepository clientRepository
            )
        {
            _projectRepository = projectRepository;
            _clientRepository = clientRepository;
            _domainRepository= domainRepository;
        }
        #region List
        public IActionResult Index()
        {
            ViewBag.BusinessNumberDropDown= _projectRepository.GetProjectList();
            ViewBag.ProjectNameDropDown=_projectRepository.GetProjectList();
           // List<ProjectDetailsViewModel> list = _projectRepository.GetProjectList();
            return View("ProjectList");
        }
        #endregion

        #region Project_List
        public IActionResult GetProjectList(string filterprojectname, string filterbusinessnumber, DateTime? filterarrivaldate)
        {
            filterprojectname = filterprojectname == null ? "" : filterprojectname;

            IEnumerable<ProjectDetailsViewModel> list = _projectRepository.GetProjectListfilter( filterprojectname, filterbusinessnumber,  filterarrivaldate);
            return PartialView("_Partial_ProjectList", list);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _projectRepository.DeleteProjectsync(id);
            return RedirectToAction("Index");
        }
        #endregion
        public IActionResult View(int id)
        {

            var project = _projectRepository.GetProjectDetails(id);
            return PartialView("_ProjectView", project);
        }

        #region Add

        public IActionResult ProjectForm()

        {
            ViewBag.BusinessNumberDropDown= _clientRepository.GetClientList();
            ViewBag.ClientDropDown = _clientRepository.GetClientList();
            ViewBag.DomainDropDown = _domainRepository.GetDomainList();
            return View();
        }
        public IActionResult Update(int id)
        {
            ViewBag.ClientDropDown = _clientRepository.GetClientList();
            ViewBag.DomainDropDown = _domainRepository.GetDomainList();
            var Project = _projectRepository.GetProjectDetails((int)id);
            return View("ProjectForm", Project);
        
        }
        public IActionResult AddEdit(ProjectDetailsViewModel model)
            {
            if (ModelState.IsValid)
            {
                if (model.ProjectId > 0)
                {
                    _projectRepository.UpdateProject(model);
                }
                else
                {
                    _projectRepository.AddProject(model);
                }
            }
            else
            {
                ViewBag.BusinessNumberDropDown = _clientRepository.GetClientList();
                ViewBag.ClientDropDown = _clientRepository.GetClientList();
                ViewBag.DomainDropDown = _domainRepository.GetDomainList();
                return View("ProjectForm");
            }
            return RedirectToAction("Index");

         }
        #endregion
    }
}
