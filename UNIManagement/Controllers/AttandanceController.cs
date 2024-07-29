using Microsoft.AspNetCore.Mvc;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Controllers
{
    public class AttandanceController : BaseController
    {
        //private readonly IAttandanceRepository _attandanceRepository;
        //public AttandanceController(IAttandanceRepository attandanceRepository)
           
        //{
        //    _attandanceRepository = attandanceRepository;
           
        //}
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add (AttandanceViewModal modal)
        {
            return View();
        }
    }
}
