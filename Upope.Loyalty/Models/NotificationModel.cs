using System;
using Upope.ServiceBase.Enums;

namespace Upope.Loyalty.Models
{
    public class NotificationModel
    {
        public NotificationType NotificationType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public int GameId { get; set; }
    }
}
