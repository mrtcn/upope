using System.Threading.Tasks;
using Upope.Identity.ViewModels;

namespace Upope.Identity.Services.Interfaces
{
    public interface IGameUserSyncService
    {
        Task<bool> SyncGameUserTable(CreateOrUpdateGameUserViewModel model, string accessToken);
    }
}
