using AutoMapper;
using Upope.Notification.Data.Entities;
using Upope.Notification.EntityParams;
using Upope.ServiceBase.Models;
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

            CreateMap<NotificationTemplate, NotificationTemplateEntityParams>();
            CreateMap<NotificationTemplateEntityParams, NotificationTemplate>();

            CreateMap<NotificationTemplateEntityParams, NotificationTemplateCulture>();
            CreateMap<NotificationTemplateCulture, NotificationTemplateEntityParams>();
        }
    }
}
