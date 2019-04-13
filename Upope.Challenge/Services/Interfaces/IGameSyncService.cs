using System.Threading.Tasks;
using Upope.Challenge.ViewModels;

namespace Upope.Game.Services.Interfaces
{
    public interface IGameSyncService
    {
        Task SyncGameTable(CreateOrUpdateGameViewModel model, string accessToken);
    }
}
