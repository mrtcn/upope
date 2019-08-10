using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upope.Notification.ViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IsActionTaken { get; set; }
    }
}
