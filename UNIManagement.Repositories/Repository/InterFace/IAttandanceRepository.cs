using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;

namespace UNIManagement.Repositories.Repository.InterFace
{
    public interface IAttandanceRepository
    {
        public void AddAttandance(int day, int month, int year, short status);
        List<AttandanceViewModal> GetAttandace(int year, int month,int EmployeeId);
    }
}
