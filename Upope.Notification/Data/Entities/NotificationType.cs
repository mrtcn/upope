using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.ServiceBase.Models;
using NotificationEntity = Upope.Notification.Data.Entities.Notification;

namespace Upope.Notification.Data.Entities
{
    public interface INotificationType : IEntity, IHasStatus, IDateOperationFields
    {
        string Title { get; set; }
        string Description { get; set; }
    }

    public class NotificationType : OperatorFields, INotificationType
    {
        [Key]
        public int Id { get; set; }
        public Status Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<NotificationEntity> Notifications { get; set; }
    }
}
