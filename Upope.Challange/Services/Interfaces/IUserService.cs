using Upope.Challange.Data.Entities;
using Upope.Challange.EntityParams;
using Upope.ServiceBase;

namespace Upope.Challange.Services.Interfaces
{
    public interface IUserService: IEntityServiceBase<User>
    {
        UserParams GetUserByUserId(string userId);
    }
}
