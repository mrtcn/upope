using System.Threading.Tasks;
using Upope.Notification.ViewModels;

namespace Upope.Notification.Services.Interfaces
{
    public interface ILoyaltySyncService
    {
        Task ChargeCredit(CreditsViewModel model, string accessToken);
        Task AddCredit(CreditsViewModel model, string accessToken);
    }
}
