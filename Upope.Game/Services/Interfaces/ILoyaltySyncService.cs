
using System.Threading.Tasks;
using Upope.Game.ViewModels;

namespace Upope.Game.Services.Interfaces
{
    public interface ILoyaltySyncService
    {
        Task ChargeCredit(CreditsViewModel model, string accessToken);
        Task AddCredit(CreditsViewModel model, string accessToken);
    }
}
