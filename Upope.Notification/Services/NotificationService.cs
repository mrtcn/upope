using Upope.Notification.Data.Entities;
using NotificationEntity = Upope.Notification.Data.Entities.Notification;
using Upope.ServiceBase;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Notification.EntityParams;
using System.Linq;

namespace Upope.Notification.Services
{
    public interface INotificationService : ICulturedEntityServiceBase<NotificationEntity, NotificationCulture>
    {
        List<NotificationEntityParams> GetNotifications(string userId);
    }

    public class NotificationService : CulturedEntityServiceBase<NotificationEntity, NotificationCulture>, INotificationService
    {
        private readonly IMapper _mapper;

        public NotificationService(
            ApplicationDbContext applicationDbContext,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _mapper = mapper;
        }

        public List<NotificationEntityParams> GetNotifications(string userId)
        {
            var notifications = Entities.Where(x => x.Status == ServiceBase.Enums.Status.Active && x.UserId == userId).ToList();
            var notificationEntityParamsList = _mapper.Map<List<NotificationEntityParams>>(notifications);

            return notificationEntityParamsList;
        }
    }
}
