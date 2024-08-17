using Microsoft.EntityFrameworkCore;
using UNIManagement.Entities.DataContext;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.CommanHelper;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Repositories.Repository
{
    public class TaskRepository : ITaskRepository
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region List
        public List<TaskViewModel> GetTaskList()
        {
            var result = (from task in _context.EmployeeTasks
                          join employee in _context.Employees
                          on task.EmployeeId equals employee.EmployeeId into EmployeeGroup
                          from emp in EmployeeGroup.DefaultIfEmpty()
                          join project in _context.Projects
                          on task.ProjectId equals project.ProjectId into ProjectGroup
                          from proj in ProjectGroup.DefaultIfEmpty()
                          where task.IsDeleted == false
                          orderby task.Modified descending
                          select new TaskViewModel
                          {
                              TaskId = task.TaskId,
                              TokenNumber = task.TokenNumber,
                              EmployeeName = emp.FirstName,
                              ProjectName = proj.Name,
                              Description = task.Description,
                              TaskAssignDate = task.TaskAssignDate,
                              Status = task.Status,
                          }).ToList();
            return result;
        }

        public List<TaskViewModel> GetTaskListfilter(string filtertokennumber, string filtleremployeename, string filterprojectname, string filterstatus, DateTime? filterdate )
        {
            List<TaskViewModel> taskList = (from task in _context.EmployeeTasks
                                            join employee in _context.Employees
                                            on task.EmployeeId equals employee.EmployeeId into EmployeeGroup
                                            from emp in EmployeeGroup.DefaultIfEmpty()
                                            join project in _context.Projects
                                            on task.ProjectId equals project.ProjectId into ProjectGroup
                                            from proj in ProjectGroup.DefaultIfEmpty()
                                            orderby task.Modified descending
                                            where (task.IsDeleted == false
                                                 && (string.IsNullOrEmpty(filtertokennumber) || task.TokenNumber.ToLower().Contains(filtertokennumber.ToLower()))
                                                 && (string.IsNullOrEmpty(filtleremployeename) || emp.FirstName.ToLower().Contains(filtleremployeename.ToLower()))
                                                 && (string.IsNullOrEmpty(filterprojectname) || proj.Name.ToLower().Contains(filterprojectname.ToLower()))
                                                 && (string.IsNullOrEmpty(filterstatus) || task.Status == filterstatus)
                                                 && (!filterdate.HasValue || task.TaskAssignDate == filterdate.Value))
                                            select new TaskViewModel
                                            {
                                                TaskId = task.TaskId,
                                                TokenNumber = task.TokenNumber,
                                                EmployeeName = emp.FirstName,
                                                ProjectName = proj.Name,
                                                Description = task.Description,
                                                TaskAssignDate = task.TaskAssignDate,
                                                Status = task.Status,
                                            }).ToList();
            return taskList;

            }

        #endregion

        #region Delete
        public async Task DeleteTasktsync(int id)
        {
            EmployeeTask? d = await _context.EmployeeTasks
                                            .Where(x => x.TaskId == id)
                                            .FirstOrDefaultAsync();
            if (d != null)
            {
                d.IsDeleted = true;
                _context.EmployeeTasks.Update(d);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Task_Add
        /// <summary>
        /// Add Employee Details
        /// </summary>
        /// <returns></returns>
        public void AddTask(TaskViewModel model)
        {
            try
            {
                var Task = new EmployeeTask();
                Task.TokenNumber = GenerateTokenName(model.ProjectName, model.EmployeeName);
                Task.ProjectId = model.ProjectId;
                Task.EmployeeId = model.EmployeeId;             
                Task.Description = model.Description;               
                Task.TaskAssignDate = model.TaskAssignDate;
                Task.DueDate = model.DueDate;
                Task.Status = model.Status;
                Task.Created = DateTime.Now;
                Task.Modified = Task.Created;
                Task.IsDeleted = false;             
                _context.EmployeeTasks.Add(Task);
                _context.SaveChanges();
                Task.Document = Helper.UploadFile(model.TaskDocument, Task.TaskId, "Task", "Document.pdf");
                Task.CreatedBy = Task.EmployeeId;
                Task.ModifiedBy = Task.EmployeeId;              
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return;
            }
        }

        #region Token Generate

        public string GenerateTokenName(string projectName, string employeeName)
        {
            string project = projectName.Substring(0, 3).ToUpper();
            string employee = employeeName.Substring(0, 3).ToUpper();
            string currentDate = DateTime.Now.ToString("ddMMyyyy");
            string currentTime = DateTime.Now.ToString("hhmmss");
            string TokenNumber = project + employee + currentDate + "_" + currentTime;

            return TokenNumber;
        }

        #endregion


        #endregion

        #region Task_Edit
        /// <summary>
        /// Update Employee Details By EmployeeId
        /// </summary>
        /// <param name="model"></param>
        public void UpdateTask(TaskViewModel model)
        {
            try
            {
                EmployeeTask? task = _context.EmployeeTasks
                                    .Where(x => x.TaskId == model.TaskId)
                                    .FirstOrDefault();

                if (task != null)
                {
                    task.ProjectId = model.ProjectId;
                    task.EmployeeId = model.EmployeeId;
                    task.Description = model.Description;
                    task.TaskAssignDate = model.TaskAssignDate;
                    task.DueDate = model.DueDate;
                    task.Status = model.Status;                   
                    task.Modified = DateTime.Now;
                    task.ModifiedBy = model.TaskId;
                    if (model.TaskDocument != null)
                        task.Document = Helper.UploadFile(model.TaskDocument, task.TaskId, "Task", "Document.pdf");                 
                    _context.EmployeeTasks.Update(task);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        #region GetTaskById
        /// <summary>
        /// Get Only one Employee Details By EmployeeId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        public TaskViewModel GetTaskDetails(int Id)
        {
            TaskViewModel? TaskDetails = (from t in _context.EmployeeTasks
                                          join emp in _context.Employees
                                          on t.EmployeeId equals emp.EmployeeId into EmployeeGroup
                                          from e in EmployeeGroup.DefaultIfEmpty()
                                          join prj in _context.Projects
                                         on t.ProjectId equals prj.ProjectId into ProjectGroup
                                          from p in ProjectGroup.DefaultIfEmpty()
                                          where t.TaskId == Id 
                                                && t.EmployeeId == e.EmployeeId
                                                && t.ProjectId == p.ProjectId
                                          select new TaskViewModel()
                                          {
                                              TaskId = t.TaskId,
                                              ProjectId = (int)t.ProjectId,
                                              ProjectName = p.Name,
                                              EmployeeId = t.EmployeeId,
                                              EmployeeName = e.FirstName,
                                              TokenNumber = t.TokenNumber,
                                              Description = t.Description,
                                              TaskAssignDate = t.TaskAssignDate,
                                              DueDate = t.DueDate,
                                              Status = t.Status,
                                             
                                          }).FirstOrDefault();
            return TaskDetails;          

        }
        #endregion


        #endregion

    }
}
