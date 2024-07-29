using Microsoft.AspNetCore.Mvc;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Controllers
{
   
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public TaskController (ITaskRepository taskRepository, IEmployeeRepository employeeRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
        }
        public IActionResult Index()
        {
            return View("TaskList");
        }
        #region Task_List
        public IActionResult GetTaskList(string filtertokennumber, string filtleremployeename, string filterprojectname, string filterstatus, DateTime? filterdate)
        {
            filtertokennumber = filtertokennumber == null ? "" : filtertokennumber;

            List<TaskViewModel> list = _taskRepository.GetTaskListfilter( filtertokennumber,  filtleremployeename,  filterprojectname, filterstatus,  filterdate);
            return PartialView("_Partial_TaskList", list);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _taskRepository.DeleteTasktsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region AddEdit 

        public IActionResult AddEdit(TaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.TaskId > 0)
                {
                    _taskRepository.UpdateTask(model);

                }
                else
                {
                    _taskRepository.AddTask(model);
                }
            }
            return RedirectToAction("Index");

        }
        #endregion

        public IActionResult View(int id)
         {
            
            var Task = _taskRepository.GetTaskDetails(id);
            return PartialView("_TaskView", Task);
        }

        #region Update
        public IActionResult Update(int id)
        {
            ViewBag.ProjectNameDropDown = _projectRepository.GetProjectList();
            ViewBag.EmployeeNameDropDown = _employeeRepository.GetEmployeeList();
            var Task = _taskRepository.GetTaskDetails((int)id);
            return View("TaskForm", Task);

        }
        #endregion

        public IActionResult TaskForm()
            {
            ViewBag.ProjectNameDropDown = _projectRepository.GetProjectList();
            ViewBag.EmployeeNameDropDown = _employeeRepository.GetEmployeeList();

            //List<EmployeeViewModel> data = _employeeRepository.GetEmployeeList();
            //ViewBag.EmployeeNameDropDown = data.ToList();
            return View();
            }
     


    }
}
