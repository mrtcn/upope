using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Upope.ServiceBase.Models;

namespace Upope.Notification.Hubs
{
    public interface ITypedHubClient
    {
        Task ReceiveNotification(NotificationModel notificationModel);
    }

    public class NotificationHub : Hub<ITypedHubClient>
    {
        public void Send(string userId, NotificationModel model)
        {
            Clients.User(userId).ReceiveNotification(model);
        }
    }
}
