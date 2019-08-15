﻿using System;
using Upope.Notification.Data.Entities;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Models;

namespace Upope.Notification.EntityParams
{
    public class NotificationTemplateEntityParams : INotificationTemplate, IEntityParams
    {
        public int Id { get; set; }
        public NotificationType NotificationType { get; set; }
        public string ImagePath { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
