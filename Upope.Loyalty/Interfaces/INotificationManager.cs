using System.Threading.Tasks;

namespace Upope.Loyalty.Interfaces
{
    public interface INotificationManager
    {
        Task SendNotification(string accessToken, string userId);
    }
}
