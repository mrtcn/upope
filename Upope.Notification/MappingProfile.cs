using AutoMapper;
using System.Collections.Generic;
using Upope.Notification.Data.Entities;
using Upope.Notification.EntityParams;
using Upope.Notification.ViewModels;
using NotificationEntity = Upope.Notification.Data.Entities.Notification;

namespace Upope.Notification
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<List<NotificationEntity>, List<NotificationEntityParams>>();

            CreateMap<NotificationEntity, NotificationEntityParams>();
            CreateMap<NotificationEntityParams, NotificationEntity>();

            CreateMap<NotificationType, NotificationTypeEntityParams>();
            CreateMap<NotificationTypeEntityParams, NotificationType>();

            CreateMap<NotificationEntityParams, NotificationViewModel>();
            CreateMap<NotificationViewModel, NotificationEntityParams>();
        }
    }
}
