using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Controllers
{
    public class EmployeeController : BaseController
    {

        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
     

        #region List
        public IActionResult Index()
        {
            List<EmployeeViewModel> list = _employeeRepository.GetEmployeeList();
            return View("EmployeeList");
        }
        #endregion

        #region Employee_List
        public IActionResult GetEmployeeList(string filterName, string filterType, DateTime? filterJoinningDate)
        {
            filterName = filterName == null ? "" : filterName;

            List<EmployeeDetailsViewModel> list = _employeeRepository.GetEmployeeListfilter(filterName, filterType, filterJoinningDate);
            return PartialView("_Partial_EmployeeList", list);
        }
        #endregion
        public IActionResult View(int id)
        {

            var employee = _employeeRepository.GetEmployeeDetails(id);
            return PartialView("_EmployeeView", employee);
        }
        #region AddEditForm_View
        [HttpGet, Route("employee/add", Name = "EmployeeAdd")]
        public IActionResult EmployeeForm()
        {
            return View();
        }
        public IActionResult AddEdit(EmployeeDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.EmployeeId > 0)
                {
                    _employeeRepository.UpdateEmployee(model);
                }
                else
                {
                    _employeeRepository.AddEmployee(model);
                }
            }
            return RedirectToAction("Index");

        }
        public IActionResult Update(int id)
        {
            var Employee = _employeeRepository.GetEmployeeDetails((int)id);

            return View("EmployeeForm", Employee);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            return RedirectToAction("Index");
        }
        #endregion

     
      
    }
}
