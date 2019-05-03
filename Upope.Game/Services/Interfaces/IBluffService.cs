using System.Threading.Tasks;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.Game.ViewModels;
using Upope.ServiceBase;

namespace Upope.Game.Services.Interfaces
{
    public interface IBluffService : IEntityServiceBase<Bluff>
    {
        BluffParams GetBluffParams(SendBluffViewModel model, string userId, GameRoundParams lastGameRound);        
    }
}
