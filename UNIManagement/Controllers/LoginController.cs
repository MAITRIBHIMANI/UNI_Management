using Microsoft.AspNetCore.Mvc;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Controllers
{
    public class LoginController : Controller
    {
        #region Constructor
        private readonly ILoginRepository _loginRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public LoginController(ILoginRepository loginRepository, IEmployeeRepository employeeRepository)
        {
            _loginRepository = loginRepository;
            _employeeRepository = employeeRepository;
        }
        #endregion
        
        #region Login Page View
        public IActionResult Index()
        {
            return View();
        }
        #endregion           

        #region Login 
        public IActionResult userLogin(User modal)
        {
            var employee = _loginRepository.GetUser(modal.Email, modal.Password);
         
            if (employee != null)
                return RedirectToAction("Index", "Home");
            else
                return RedirectToAction("Index");
        }
        #endregion

    }
}