using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNIManagement.Entities.ViewModel;

namespace UNIManagement.Repositories.Repository.InterFace
{
    public interface INotificationRepository
    {
       List<NotificationViewModel> GetNotificationList();
        Task DeleteNotificationAsync(int NotificationId);

        public void AddNotification(NotificationViewModel model);
       NotificationViewModel? GetNotificationsByNotificationsId(int NotificationsId);
        public void UpdateNotification(NotificationViewModel model);

    }
}
