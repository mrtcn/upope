using Upope.Challenge.Data.Entities;
using Upope.Challenge.EntityParams;
using Upope.ServiceBase;

namespace Upope.Challenge.Services.Interfaces
{
    public interface IUserService: IEntityServiceBase<User>
    {
        UserParams GetUserByUserId(string userId);
    }
}
