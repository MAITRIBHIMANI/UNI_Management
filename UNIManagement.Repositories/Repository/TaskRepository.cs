using Microsoft.Build.ObjectModelRemoting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using UNIManagement.Entities.DataContext;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.CommanHelper;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Repositories.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

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


                          select new TaskViewModel
                          {
                              TaskId = task.TaskId,
                              EmployeeName = emp.FirstName,
                              ProjectName = proj.Name,
                              Description = task.Description,
                              TaskAssignDate = task.TaskAssignDate,
                              Status = task.Status,

                          }).ToList();
            return result;
        }


        public List<TaskViewModel> GetTaskListfilter(string filtertokennumber, string filtleremployeename, string filterprojectname, string filterstatus, DateTime? filterdate)
        {
            List<TaskViewModel> taskList = (from task in _context.EmployeeTasks
                                            join employee in _context.Employees
                                            on task.EmployeeId equals employee.EmployeeId into EmployeeGroup
                                            from emp in EmployeeGroup.DefaultIfEmpty()
                                            join project in _context.Projects
                                            on task.ProjectId equals project.ProjectId into ProjectGroup
                                            from proj in ProjectGroup.DefaultIfEmpty()
                                            where (task.IsDeleted == false
                                                 && (string.IsNullOrEmpty(filtertokennumber) || task.TokenNumber.ToLower().Contains(filtertokennumber.ToLower()))
                                                 && (string.IsNullOrEmpty(filtleremployeename) || emp.FirstName.ToLower().Contains(filtleremployeename.ToLower()))
                                                 && (string.IsNullOrEmpty(filterprojectname) || proj.Name.ToLower().Contains(filterprojectname.ToLower()))
                                                 && (string.IsNullOrEmpty(filterstatus) || task.Status.ToLower().Contains(filterstatus.ToLower()))
                                                 && (!filterdate.HasValue || task.TaskAssignDate == filterdate.Value)
                                                 )
                                            select new TaskViewModel
                                            {
                                                TaskId = task.TaskId,
                                                TokenNumber = task.TokenNumber,
                                                EmployeeName = emp.FirstName,
                                                ProjectName = proj.Name,
                                                Description = task.Description,
                                                TaskAssignDate = task.TaskAssignDate,
                                                Status = task.Status,

                                            }
                                         ).ToList();
            return taskList;

            }
        public async Task DeleteTasktsync(int id)
        {
            EmployeeTask d = await _context.EmployeeTasks.Where(x => x.TaskId == id).FirstOrDefaultAsync();
            if (d != null)
            {
                d.IsDeleted = true;
                _context.EmployeeTasks.Update(d);
                await _context.SaveChangesAsync();
            }
        }


        #region Task_Add
        /// <summary>
        /// Add Employee Details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void AddTask(TaskViewModel model)
        {
            try
            {
                var Task = new EmployeeTask();
                Task.ProjectId = model.ProjectId;
                Task.EmployeeId = model.EmployeeId;
                Task.Description = model.Description;
                Task.TokenNumber = GenerateTokenName(model.ProjectName,model.EmployeeName);
                Task.TaskAssignDate = model.TaskAssignDate;
                Task.DueDate = model.DueDate;
                Task.Status = model.Status;
                Task.Created = DateTime.Now;
                Task.IsDeleted = false;
                _context.EmployeeTasks.Add(Task);
                _context.SaveChanges();

                Task.Document = Helper.TaskDocument(model.TaskDocument, Task.TaskId, "Task", "Document.pdf");
                Task.CreatedBy = Task.TaskId;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return;
            }


        }

        #endregion


        public string GenerateTokenName(string projectName, string employeeName)
        {
            string project = projectName.Substring(0, 3).ToUpper();
            string employee = employeeName.Substring(0, 3).ToUpper();
            string currentDate = DateTime.Now.ToString("ddMMyyyy");
            string currentTime = DateTime.Now.ToString("hhmmss");
            string TokenNumber = project + employee + currentDate + "_" + currentTime;
            return TokenNumber;

        }


        #region Task_Edit
        /// <summary>
        /// Update Employee Details By EmployeeId
        /// </summary>
        /// <param name="model"></param>
        public void UpdateTask(TaskViewModel model)
        {
            try
            {
                EmployeeTask task = _context.EmployeeTasks
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
                        task.Document = Helper.TaskDocument(model.TaskDocument, task.TaskId, "Task", "Document.pdf");
                 
                    _context.EmployeeTasks.Update(task);
                    _context.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                return;
            }

        }
        #endregion

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
                                          where t.TaskId == Id && t.EmployeeId == e.EmployeeId
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
                                          }

                ).FirstOrDefault();
            return TaskDetails;
            //var task = _context.EmployeeTasks
            //               .FirstOrDefault(x => x.TaskId == Id);
            //if (task != null)
            //{
            //    return new TaskViewModel()
            //    {
            //        TaskId = task.TaskId,
            //        ProjectId = task.ProjectId,
            //        EmployeeId = task.EmployeeId,
            //        TokenNumber = task.TokenNumber,
            //        Description = task.Description,
            //        TaskAssignDate = task.TaskAssignDate,
            //        DueDate = task.DueDate,
            //        Status = task.Status,

            //    };
            //}
            //return new TaskViewModel();

        }


        #endregion
    }
}
