using Microsoft.AspNetCore.Mvc;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Controllers
{
    public class NotificationController : BaseController
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public IActionResult Index()
        {
         List<NotificationViewModel> list = _notificationRepository.GetNotificationList();

            return View("NotificationList", list);
        }
        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _notificationRepository.DeleteNotificationAsync(id);
            return RedirectToAction("Index");
        }
        #endregion
        public IActionResult NotificationForm()
        {
            return View();
        }
        [HttpPost, Route("notification/addedit", Name = "NotificationAddEdit")]
        public IActionResult Add(NotificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.NotificationId > 0)
                {
                    _notificationRepository.UpdateNotification(model);

                }
                else
                {
                    _notificationRepository.AddNotification(model);

                }
            }
            return RedirectToAction("Index");

        }

        public IActionResult GetNotificationDetails(string? isEdit, int? notificationId)
        {
            var NotificationViewModel = new NotificationViewModel() ;
            if (isEdit != "0")
            {
                NotificationViewModel = _notificationRepository.GetNotificationsByNotificationsId((int)notificationId);

            }
            return PartialView("_notificationForm", NotificationViewModel);
        }
    }
}
