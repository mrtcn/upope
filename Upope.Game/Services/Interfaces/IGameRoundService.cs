using System.Threading.Tasks;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.ServiceBase;

namespace Upope.Game.Services.Interfaces
{
    public interface IGameRoundService : IEntityServiceBase<GameRound>
    {
        Task SendChoice(SendChoiceParams sendChoiceParams);
    }
}
