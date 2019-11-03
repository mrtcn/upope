using System.Threading.Tasks;
using Upope.Game.Models;
using Upope.Game.Services.Models;
using Upope.ServiceBase;
using GameEntity = Upope.Game.Data.Entities.Game;

namespace Upope.Game.Services.Interfaces
{
    public interface IGameService : IEntityServiceBase<GameEntity>
    {
        RematchUserInfo RematchUserInfo(string userId, string requestingUserId, int credit, int maxCredit);
        Task SendGameCreatedMessage(GameCreatedModel model);
        bool IsHostUser(int gameId, string userId);
        int StreakCount(string userId);
        int LatestGameId(string userId);
        int LatestWinGameId(string userId);
    }
}
