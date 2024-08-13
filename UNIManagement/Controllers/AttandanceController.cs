using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Controllers
{
    public class AttandanceController : BaseController
    {
        #region Constructor
        private readonly IAttandanceRepository _attandanceRepository;
        public AttandanceController(IAttandanceRepository attandanceRepository)

        {
            _attandanceRepository = attandanceRepository;

        }
        #endregion

        #region GetAttandace
        public IActionResult GetAttandaceForMonth(int year, int month,int EmaployeeId)
        {           
            var v =  _attandanceRepository.GetAttandace((int)year, (int)month, EmaployeeId=131);
            return Json(v);
        }
        #endregion

        #region Add Attendance
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SaveAttendance(int day, int month, int year, short status)
        {
            _attandanceRepository.AddAttandance(day, month, year, status);
            return Ok(new { message = "Attendance saved successfully" });           
        }
        #endregion
    }
}
