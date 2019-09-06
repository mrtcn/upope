using System;
using Upope.ServiceBase.Enums;

namespace Upope.ServiceBase.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImagePath { get; set; }
        public string UserId { get; set; }
        public int GameId { get; set; }
        public bool IsActionTaken { get; set; }
        public int? CreditsToEarn { get; set; }
        public int? UserCredits { get; set; }
        public int? WinStreakCount { get; set; }
    }
}
