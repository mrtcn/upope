using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.Game.Services.Models;
using Upope.ServiceBase;

namespace Upope.Game.Services.Interfaces
{
    public interface IGameRoundService : IEntityServiceBase<GameRound>
    {
        bool IsRoundOver(int gameRoundId);
        bool IsGameOver(int gameId);
        string GameWinnerId(int gameId);
        RoundResult CalculateRoundEnd(int gameRoundId);
        GameScore CalculateGameEnd(int gameId);
    }
}
