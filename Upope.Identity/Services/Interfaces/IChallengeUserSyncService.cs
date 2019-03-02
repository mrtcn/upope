using System;
using System.Threading.Tasks;
using Upope.Identity.ViewModels;

namespace Upope.Identity.Services.Interfaces
{
    public interface IChallengeUserSyncService
    {
        Task SyncChallengeUserTable(CreateOrUpdateChallengeUserViewModel model, string accessToken);
    }
}
