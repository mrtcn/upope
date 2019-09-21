using Upope.Chat.Data.Entities;
using Upope.Chat.EntityParams;
using Upope.ServiceBase;

namespace Upope.Chat.Services.Interfaces
{
    public interface IUserService: IEntityServiceBase<User>
    {
        UserParams GetUserByUserId(string userId);
    }
}
