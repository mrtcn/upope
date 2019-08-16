using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.Notification.Data.Entities
{
    public interface INotificationTemplate : IEntity, IHasStatus, IDateOperationFields
    {
        NotificationType NotificationType { get; set; }
        string ImagePath { get; set; }
    }

    public class NotificationTemplate : OperatorFields, INotificationTemplate, IHasCulturedEntities<NotificationTemplateCulture>
    {
        [Key]
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public Status Status { get; set; }
        public List<NotificationTemplateCulture> CulturedEntities { get; set; }
        public NotificationType NotificationType { get; set; }
    }

    public interface INotificationTemplateCulture : IHasStatus, IHasCulturedEntityStatus, IHasParent<NotificationTemplate>
        , ICulturedEntity, IDateOperationFields
    {
        string Label { get; set; }
        string Description { get; set; }
    }

    public class NotificationTemplateCulture : OperatorFields, INotificationTemplateCulture
    {
        [Key]
        public int Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Status CulturedEntityStatus { get; set; }
        public string Culture { get; set; }
        public NotificationTemplate BaseEntity { get; set; }
        public int BaseEntityId { get; set; }
    }
}
