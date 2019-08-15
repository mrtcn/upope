using NotificationEntity = Upope.Notification.Data.Entities.Notification;
using Upope.ServiceBase;
using AutoMapper;
using Upope.Notification.EntityParams;
using System.Collections.Generic;
using Upope.ServiceBase.Enums;
using System.Linq;

namespace Upope.Notification.Services
{
    public interface INotificationService : IEntityServiceBase<NotificationEntity>
    {
        List<NotificationEntityParams> GetNotifications(string userId, int entityCountToSkip);
    }

    public class NotificationService : EntityServiceBase<NotificationEntity>, INotificationService
    {
        private readonly IMapper _mapper;

        public NotificationService(
            ApplicationDbContext applicationDbContext,
            IMapper mapper) : base(applicationDbContext, mapper)
        {

        }

        public List<NotificationEntityParams> GetNotifications(string userId, int entityCountToSkip)
        {
            var notifications = Entities
                .Where(x => x.Status == Status.Active && x.UserId == userId)
                .OrderByDescending(x => x.Id)
                .Skip(entityCountToSkip).ToList();

            var notificationEntityParamsList = _mapper.Map<List<NotificationEntityParams>>(notifications);

            return notificationEntityParamsList;
        }
    }
}
