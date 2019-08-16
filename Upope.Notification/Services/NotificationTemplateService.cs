using Upope.Notification.Data.Entities;
using Upope.ServiceBase;
using AutoMapper;
using Upope.Notification.EntityParams;
using System.Linq;
using Upope.ServiceBase.Enums;
using System.Globalization;
using System.Threading;

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
            CultureInfo uiCultureInfo = Thread.CurrentThread.CurrentUICulture;
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            var culture = cultureInfo.ToString();

            var notificationTemplate = Entities
                .FirstOrDefault(x => x.NotificationType == notificationType && x.Status == Status.Active);

            var notificationTemplateCulture = CulturedEntities.FirstOrDefault(x => x.BaseEntityId == notificationTemplate.Id && x.Culture == culture);

            var notificationTemplateEntityParams = Map<NotificationTemplateEntityParams>(notificationTemplateCulture.Id, culture);

            return notificationTemplateEntityParams;
        }
    }
}
