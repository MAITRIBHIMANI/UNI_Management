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


        [HttpPost]
        public IActionResult userLogin(User modal)
        {
            if (ModelState.IsValid)
            {
                bool isUser = _loginRepository.GetUser(modal.Email, modal.Password);
                if (isUser)
                {
                    HttpContext.Session.SetString("Email", modal.Email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    
                }
            }
            return View("Index");
        }
        #region Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        #endregion

    }
}