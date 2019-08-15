using Upope.Notification.Data.Entities;
using Upope.ServiceBase;
using AutoMapper;
using Upope.Notification.EntityParams;
using System.Linq;
using Upope.ServiceBase.Enums;

namespace Upope.Notification.Services
{
    public interface INotificationTemplateService : ICulturedEntityServiceBase<NotificationTemplate, NotificationTemplateCulture>
    {
        NotificationTemplateEntityParams GetNotificationTemplate(NotificationType notificationType);
    }

    public class NotificationTemplateService : CulturedEntityServiceBase<NotificationTemplate, NotificationTemplateCulture>, INotificationTemplateService
    {
        private readonly IMapper _mapper;

        public NotificationTemplateService(
            ApplicationDbContext applicationDbContext,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _mapper = mapper;
        }

        public NotificationTemplateEntityParams GetNotificationTemplate(NotificationType notificationType)
        {
            var notificationTemplate = Entities
                .FirstOrDefault(x => x.NotificationType == notificationType && x.Status == Status.Active);

            var notificationEntityParamsList = _mapper.Map<NotificationTemplateEntityParams>(notificationTemplate);

            return notificationEntityParamsList;
        }
    }
}
