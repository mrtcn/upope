using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.Notification.Data.Entities
{
    public interface INotification : IEntity, IHasStatus, IDateOperationFields
    {
        int NotificationTypeId { get; set; }
        string ImagePath { get; set; }
        string UserId { get; set; }
        bool IsActionTaken { get; set; }
    }

    public class Notification : OperatorFields, INotification, IHasCulturedEntities<NotificationCulture>
    {
        [Key]
        public int Id { get; set; }
        public int NotificationTypeId { get; set; }
        public string ImagePath { get; set; }
        public string UserId { get; set; }
        public bool IsActionTaken { get; set; }
        public Status Status { get; set; }
        public List<NotificationCulture> CulturedEntities { get; set; }
        public NotificationType NotificationType { get; set; }
    }

    public interface INotificationCulture : IHasStatus, IHasCulturedEntityStatus, IHasParent<Notification>
        , ICulturedEntity, IDateOperationFields
    {        
        string Label { get; set; }
        string Description { get; set; }
    }

    public class NotificationCulture : OperatorFields, INotificationCulture
    {
        [Key]
        public int Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }        
        public Status Status { get; set; }
        public Status CulturedEntityStatus { get; set; }
        public Culture Culture { get; set; }
        public Notification BaseEntity { get; set; }
        public int BaseEntityId { get; set; }
    }
}
