using Microsoft.AspNetCore.Mvc;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Controllers
{
    public class NotificationController : BaseController
    {
        #region Constructor 
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        #endregion

        #region Notification_List
        public IActionResult Index()
        {
            List<NotificationViewModel> list = _notificationRepository.GetNotificationList();

            return View("NotificationList", list);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete Notification By Id
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            await _notificationRepository.DeleteNotificationAsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region AddEdit
        public IActionResult NotificationForm()
        {
            return View();
        }

        /// <summary>
        ///  Add Edit Condition of Notification
        /// </summary>
        /// <returns></returns>

        [HttpPost, Route("notification/addedit", Name = "NotificationAddEdit")]
        public IActionResult Add(NotificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.NotificationId > 0)
                    _notificationRepository.UpdateNotification(model);
                else
                    _notificationRepository.AddNotification(model);
            }
            return RedirectToAction("Index");

        }

        /// <summary>
        /// Get Notification Details BY Id
        /// </summary>
        /// <returns></returns>
        public IActionResult GetNotificationDetails(string? isEdit, int? notificationId)
        {
            var NotificationViewModel = new NotificationViewModel();
            if (isEdit != "0")
                NotificationViewModel = _notificationRepository.GetNotificationsByNotificationsId((int)notificationId);
            return PartialView("_notificationForm", NotificationViewModel);
        }
        #endregion
    }
}