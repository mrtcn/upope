using System;
using System.Threading.Tasks;
using Upope.Identity.ViewModels;

namespace Upope.Identity.Services.Interfaces
{
    public interface ILoyaltySyncService
    {
        Task SyncLoyaltyTable(CreateOrUpdateLoyaltyViewModel model, string accessToken);
    }
}
