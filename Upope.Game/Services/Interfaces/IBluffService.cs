using System.Threading.Tasks;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.Game.Enum;
using Upope.Game.ViewModels;
using Upope.ServiceBase;

namespace Upope.Game.Services.Interfaces
{
    public interface IBluffService : IEntityServiceBase<Bluff>
    {
        BluffParams GetBluffParams(SendBluffViewModel model, string userId, RockPaperScissorsType choice);
        Task AskBluff(string userId, int gameId, int gameRoundId);
        Task TextBluff(SendBluffViewModel model, string userId);
    }
}
