using System;
using Upope.ServiceBase.Enums;

namespace Upope.Notification.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string UserId { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IsActionTaken { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
