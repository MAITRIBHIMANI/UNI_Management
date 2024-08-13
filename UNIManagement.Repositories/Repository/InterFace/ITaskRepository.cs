using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNIManagement.Entities.ViewModel;

namespace UNIManagement.Repositories.Repository.InterFace
{
    public interface ITaskRepository
    {
        List<TaskViewModel> GetTaskList();
        Task DeleteTasktsync(int Id);
       
        public void AddTask(TaskViewModel model);
        public void UpdateTask(TaskViewModel model);
        public TaskViewModel GetTaskDetails(int TaskId);

       
        List<TaskViewModel> GetTaskListfilter(string filtertokennumber, string filtleremployeename, string filterprojectname, string filterstatus, DateTime? filterdate );

    }
}
