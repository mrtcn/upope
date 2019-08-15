using Upope.Game.Services.Models;
using Upope.ServiceBase;
using GameEntity = Upope.Game.Data.Entities.Game;

namespace Upope.Game.Services.Interfaces
{
    public interface IGameService : IEntityServiceBase<GameEntity>
    {
        void SendGameCreatedMessage(GameCreatedModel model);
        bool IsHostUser(int gameId, string userId);
        int StreakCount(string userId);
        int LatestGameId(string userId);
        int LatestWinGameId(string userId);
    }
}
