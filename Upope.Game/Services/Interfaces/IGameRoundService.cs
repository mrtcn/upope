using System.Threading.Tasks;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.Game.ViewModels;
using Upope.ServiceBase;

namespace Upope.Game.Services.Interfaces
{
    public interface IGameRoundService : IEntityServiceBase<GameRound>
    {
        GameRoundParams GetLatestRound(int gameId, string userId);
        Task<GameRoundParams> SendChoice(SendChoiceParams sendChoiceParams);
        bool IsFirstAnswer(GameRoundParams gameRoundParams);
        Task AskBluff(string userId, GameRoundParams gameRoundParams);
        Task TextBluff(SendBluffViewModel model, string userId);
    }
}
