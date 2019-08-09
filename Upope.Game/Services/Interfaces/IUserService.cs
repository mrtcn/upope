using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.ServiceBase;

namespace Upope.Game.Services.Interfaces
{
    public interface IUserService: IEntityServiceBase<User>
    {
        UserParams GetUserByUserId(string userId);
    }
}
