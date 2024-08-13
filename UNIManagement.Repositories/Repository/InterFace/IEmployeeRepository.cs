using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.DataContext;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository.InterFace;
namespace UNIManagement.Repositories.Repository.InterFace
{
	public interface IEmployeeRepository
	{

        List<EmployeeViewModel> GetEmployeeList();
        Task DeleteEmployeeAsync(int EmployeeId);
        public void AddEmployee(EmployeeDetailsViewModel model);
        public void UpdateEmployee(EmployeeDetailsViewModel model);
        public EmployeeDetailsViewModel GetEmployeeDetails(int EmployeeId);
        List<EmployeeDetailsViewModel> GetEmployeeListfilter(string filterName, string filterType, DateTime? filterJoinningDate, string filterIsActive);
        List<Employee> isEmailExist(string Email);

    }
}
