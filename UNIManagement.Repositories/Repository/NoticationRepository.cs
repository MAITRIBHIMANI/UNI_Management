using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNIManagement.Entities.DataContext;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.CommanHelper;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Repositories.Repository
{
    public class NoticationRepository : INotificationRepository
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        public NoticationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region List
        public List<NotificationViewModel> GetNotificationList()
        {
            return _context.Notifications
                           .Where(x => x.IsDeleted == false)
                           .Select(cont => new NotificationViewModel()
                           {
                               NotificationId = cont.NotificationId,
                               Name = cont.Name,
                               Document = cont.Document,
                               Date = cont.Date,
                               Duration = cont.Duration,
                           }).ToList();
        }

        #endregion

        #region Delete
        /// <summary>
        /// Delete Emplyoee details from database on EmployeeId
        /// </summary>
        /// <returns></returns>
        public async Task DeleteNotificationAsync(int id)
        {
            Notification? d = await _context.Notifications
                                            .Where(x => x.NotificationId == id)
                                            .FirstOrDefaultAsync();
            if (d != null)
            {
                d.IsDeleted = true;
                _context.Notifications.Update(d);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region GetNotificationsByNotificationsId
        /// <summary>
        /// Retive Data From Notification Data Based On NotificationsId
        /// </summary>
        /// <returns></returns>
        public NotificationViewModel? GetNotificationsByNotificationsId(int NotificationsId)
        {
            return _context.Notifications
                           .Where(x => x.NotificationId == NotificationsId)
                           .Select(cont => new NotificationViewModel
                           {
                               NotificationId = cont.NotificationId,
                               Name = cont.Name,
                               Document = cont.Document,
                               Date = cont.Date,
                               IsActive = cont.IsActive,
                               Duration = cont.Duration,

                           }).FirstOrDefault();
        }

        #endregion

        #region Add
        public void AddNotification(NotificationViewModel model)
        {
            try
            {
                var Notification = new Notification();

                Notification.Name = model.Name;
                Notification.Date = model.Date;
                Notification.Duration = model.Duration;
                Notification.IsActive = model.IsActive;
                Notification.Created = DateTime.Now;
                Notification.IsActive = model.IsActive;
                _context.Notifications.Add(Notification);
                _context.SaveChanges();
                Notification.Document = model.Document;
                Notification.IsDeleted = false;
                Notification.Document = Helper.UploadFile(model.DocumentFile, Notification.NotificationId, "Notification", "Document.pdf");
                Notification.CreatedBy = Notification.NotificationId;
                _context.SaveChanges();
                Notification.Modified = Notification.Created;
                Notification.ModifiedBy = Notification.NotificationId;
            }
            catch (Exception e)
            {
                return;
            }
        }
        #endregion

        #region Update

        public void UpdateNotification(NotificationViewModel model)
        {
            Notification? notification = _context.Notifications
                                                 .Where(x => x.NotificationId == model.NotificationId)
                                                 .FirstOrDefault();
            if (notification != null)
            {
                notification.Name = model.Name;
                notification.Date = model.Date;
                notification.Duration = model.Duration;
                notification.IsActive = model.IsActive;
                if (model.DocumentFile != null)
                {
                    notification.Document = Helper.UploadFile(model.DocumentFile, notification.NotificationId, "Notification", "Document.pdf");
                }
                _context.Notifications.Update(notification);
                _context.SaveChanges();
            }
        }
        #endregion
    }

}
