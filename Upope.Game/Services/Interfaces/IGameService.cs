using Upope.ServiceBase;
using GameEntity = Upope.Game.Data.Entities.Game;

namespace Upope.Game.Services.Interfaces
{
    public interface IGameService : IEntityServiceBase<GameEntity>
    {
    }
}
