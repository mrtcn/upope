using Upope.Notification.Data.Entities;
using Upope.ServiceBase;
using AutoMapper;

namespace Upope.Notification.Services
{
    public interface INotificationTypeService : IEntityServiceBase<NotificationType>
    {
    }

    public class NotificationTypeService : EntityServiceBase<NotificationType>, INotificationTypeService
    {
        private readonly IMapper _mapper;

        public NotificationTypeService(
            ApplicationDbContext applicationDbContext,
            IMapper mapper) : base(applicationDbContext, mapper)
        {

        }
    }
}
