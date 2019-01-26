using System;
using System.Collections.Generic;
using MrCMS.Entities.Notifications;
using MrCMS.Helpers;
using MrCMS.Services;
using MrCMS.Web.Apps.Admin.Models.Notifications;
using NHibernate;
using NHibernate.Transform;

namespace MrCMS.Web.Apps.Admin.Services
{
    public class PersistentNotificationUIService : IPersistentNotificationUIService
    {
        private readonly ISession _session;
        private readonly IGetCurrentUser _getCurrentUser;

        public PersistentNotificationUIService(ISession session,  IGetCurrentUser getCurrentUser)
        {
            _session = session;
            _getCurrentUser = getCurrentUser;
        }

        public IList<NotificationModel> GetNotifications()
        {
            var user = _getCurrentUser.Get();
            var queryOver = _session.QueryOver<Notification>();

            if (user.LastNotificationReadDate.HasValue)
                queryOver = queryOver.Where(notification => notification.CreatedOn >= user.LastNotificationReadDate);

            NotificationModel notificationModelAlias = null;
            return queryOver.SelectList(
                builder =>
                    builder.Select(notification => notification.Message)
                        .WithAlias(() => notificationModelAlias.Message)
                        .Select(notification => notification.CreatedOn)
                        .WithAlias(() => notificationModelAlias.DateValue))
                .OrderBy(notification => notification.CreatedOn).Desc
                .TransformUsing(Transformers.AliasToBean<NotificationModel>())
                .Take(15)
                .Cacheable()
                .List<NotificationModel>();
        }

        public int GetNotificationCount()
        {
            var user = _getCurrentUser.Get();
            var queryOver = _session.QueryOver<Notification>();

            if (user.LastNotificationReadDate.HasValue)
                queryOver = queryOver.Where(notification => notification.CreatedOn >= user.LastNotificationReadDate);

            return queryOver.RowCount();
        }

        public void MarkAllAsRead()
        {
            var user = _getCurrentUser.Get();
            user.LastNotificationReadDate = DateTime.UtcNow;
            _session.Transact(session => session.Update(user));
        }
    }
}