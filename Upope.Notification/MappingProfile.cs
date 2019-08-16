using AutoMapper;
using System.Collections.Generic;
using Upope.Notification.Data.Entities;
using Upope.Notification.EntityParams;
using Upope.Notification.Models;
using Upope.Notification.ViewModels;
using NotificationEntity = Upope.Notification.Data.Entities.Notification;

namespace Upope.Notification
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NotificationModel, NotificationEntityParams>();

            CreateMap<NotificationEntity, NotificationModel>();
            CreateMap<NotificationModel, NotificationEntity>();

            CreateMap<NotificationEntity, NotificationEntityParams>();
            CreateMap<NotificationEntityParams, NotificationEntity>();

            CreateMap<NotificationEntityParams, NotificationViewModel>();
            CreateMap<NotificationViewModel, NotificationEntityParams>();

            CreateMap<NotificationTemplate, NotificationTemplateEntityParams>();
            CreateMap<NotificationTemplateEntityParams, NotificationTemplate>();

            CreateMap<NotificationTemplateEntityParams, NotificationTemplateCulture>();
            CreateMap<NotificationTemplateCulture, NotificationTemplateEntityParams>();
        }
    }
}
