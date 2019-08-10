using System;
using Upope.Notification.Data.Entities;
using NotificationEntity = Upope.Notification.Data.Entities.Notification;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.Models;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.Notification.EntityParams
{
    public class NotificationTypeEntityParams : INotificationCulture, IEntityParams, IHasCulture
        , IHasCulturedEntityStatus
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Status CulturedEntityStatus { get; set; }
        public NotificationEntity BaseEntity { get; set; }
        public int BaseEntityId { get; set; }
        public int RelatedEntityId { get; set; }
        public int Id { get; set; }
        public Culture Culture { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
