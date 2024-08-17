using Microsoft.AspNetCore.Mvc;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Controllers
{
  
    public class TaskController : Controller
    {
        #region Constructor
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public TaskController (ITaskRepository taskRepository, IEmployeeRepository employeeRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Task_List
        public IActionResult Index()
        {
            return View("TaskList");
        }
        /// <summary>
        /// Partial List and Filtered List 
        /// </summary>
        /// <returns></returns>
     
        public IActionResult GetTaskList(string filtertokennumber, string filtleremployeename, string filterprojectname, string filterstatus, DateTime? filterdate)
        {
            filtertokennumber = filtertokennumber == null ? "" : filtertokennumber;

            List<TaskViewModel> list = _taskRepository.GetTaskListfilter( filtertokennumber,  filtleremployeename,  filterprojectname, filterstatus,  filterdate);
            return PartialView("_Partial_TaskList", list);
        }       
      
        #endregion

        #region Delete
        /// <summary>
        /// Delete Task By TaskId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            await _taskRepository.DeleteTasktsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region AddEdit Task
        /// <summary>
        /// Task Form Method
        /// </summary>
        /// <returns></returns>
        public IActionResult TaskForm()
        {
            ViewBag.ProjectNameDropDown = _projectRepository.GetProjectList();
            ViewBag.EmployeeNameDropDown = _employeeRepository.GetEmployeeList();
            return View();
        }
        /// <summary>
        /// Add Edit Condition
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 

        public IActionResult AddEdit(TaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.TaskId > 0)               
                    _taskRepository.UpdateTask(model);                
                else
                    _taskRepository.AddTask(model);                
            }
            else
            {
                ViewBag.ProjectNameDropDown = _projectRepository.GetProjectList();
                ViewBag.EmployeeNameDropDown = _employeeRepository.GetEmployeeList();
                return View("TaskForm");
            }
            return RedirectToAction("Index");

        }
        #endregion

        #region Task View
        /// <summary>
        /// View Modal Pop
        /// </summary>
        /// <returns></returns>
        public IActionResult View(int id)
         {            
            var Task = _taskRepository.GetTaskDetails(id);
            return PartialView("_TaskView", Task);
        }

        #endregion

        #region Update Task
        /// <summary>
        /// Get Details By TaskId
        /// </summary>
        /// <returns></returns>
        public IActionResult Update(int id)
        {
            ViewBag.ProjectNameDropDown = _projectRepository.GetProjectList();
            ViewBag.EmployeeNameDropDown = _employeeRepository.GetEmployeeList();
            var Task = _taskRepository.GetTaskDetails((int)id);
            return View("TaskForm", Task);
        }
    
        #endregion
    }
}
