using System;
using NotificationEntity = Upope.Notification.Data.Entities.Notification;
using Upope.Notification.Data.Entities;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.Models;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.Notification.EntityParams
{
    public class NotificationEntityParams : INotification, IEntityParams, IHasCulture
        , IHasCulturedEntityStatus
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public Status Status { get; set; }
        public Culture Culture { get; set; }
        public Status CulturedEntityStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IsActionTaken { get; set; }
        public NotificationEntity BaseEntity { get; set; }
        public int BaseEntityId { get; set; }
        public int RelatedEntityId { get; set; }
        public int NotificationTypeId { get; set; }
        public string UserId { get; set; }
    }
}
