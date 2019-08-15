using System.ComponentModel.DataAnnotations;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.Notification.Data.Entities
{
    public interface INotification : IEntity, IHasStatus, IDateOperationFields
    {
        NotificationType NotificationType { get; set; }
        string Label { get; set; }
        string Description { get; set; }
        string ImagePath { get; set; }
        string UserId { get; set; }
        int GameId { get; set; }
        bool IsActionTaken { get; set; }
    }

    public class Notification : OperatorFields, INotification
    {
        [Key]
        public int Id { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string UserId { get; set; }
        public int GameId { get; set; }
        public bool IsActionTaken { get; set; }
        public Status Status { get; set; }
    }
}
