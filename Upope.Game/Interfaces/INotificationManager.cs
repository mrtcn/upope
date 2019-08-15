using System.Threading.Tasks;

namespace Upope.Game.Interfaces
{
    interface INotificationManager
    {
        Task SendNotification(string accessToken, string userId);
    }
}
