using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Upope.Notification.Models;

namespace Upope.Notification.Hubs
{
    public interface ITypedHubClient
    {
        Task ReceiveNotification(NotificationModel model);
    }

    public class NotificationHub : Hub<ITypedHubClient>
    {
        public void Send(string userId, NotificationModel model)
        {
            Clients.User(userId).ReceiveNotification(model);
        }
    }
}
