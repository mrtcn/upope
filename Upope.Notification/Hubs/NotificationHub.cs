using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Upope.Notification.ViewModels;

namespace Upope.Notification.Hubs
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(string userId, NotificationViewModel model);
    }

    public class NotificationHub : Hub<ITypedHubClient>
    {
        public void Send(string userId, NotificationViewModel model)
        {
            Clients.User(userId).BroadcastMessage(userId, model);
        }
    }
}
