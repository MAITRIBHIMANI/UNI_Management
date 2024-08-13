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
        #region Constructor
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region List
        public IActionResult Index()
        {
            ViewBag.EmpTypeDropDown = _employeeRepository.GetEmployeeList();
            List<EmployeeViewModel> list = _employeeRepository.GetEmployeeList();
            return View("EmployeeList");
        }
        #endregion

        #region Employee_List
        public IActionResult GetEmployeeList(string filterName, string filterType, DateTime? filterJoinningDate, string filterIsActive)
        {
            filterName = filterName == null ? "" : filterName;

            List<EmployeeDetailsViewModel> list = _employeeRepository.GetEmployeeListfilter(filterName, filterType, filterJoinningDate, filterIsActive);
            return PartialView("_Partial_EmployeeList", list);
        }
        #endregion

        #region Employee_View
        public IActionResult View(int id)
        {
            var employee = _employeeRepository.GetEmployeeDetails(id);
            return PartialView("_EmployeeView", employee);
        }

        #endregion

        #region AddEditForm
        [HttpGet, Route("employee/add", Name = "EmployeeAdd")]
        public IActionResult EmployeeForm()
        {
            return View();
        }
        /// <summary>
        /// Add Edit Condition of Employee
        /// </summary>
        /// <returns></returns>
        public IActionResult AddEdit(EmployeeDetailsViewModel model)
        {
            if (_employeeRepository.isEmailExist(model.Email).Count > 0)
            {
                ModelState.AddModelError("Email", "Email is Already Exist!");
                return View("EmployeeForm");
            }
            if (ModelState.IsValid)
            {
                if (model.EmployeeId > 0)
                    _employeeRepository.UpdateEmployee(model);
                else
                    _employeeRepository.AddEmployee(model);
            }
            else
            {
                return View("EmployeeForm");
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Get Employee Details BY EMployeeID
        /// </summary>
        /// <returns></returns>
        public IActionResult Update(int id)
        {
            var Employee = _employeeRepository.GetEmployeeDetails((int)id);

            return View("EmployeeForm", Employee);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete Employee By EmployeeId
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            return RedirectToAction("Index");
        }
        #endregion



    }
}
