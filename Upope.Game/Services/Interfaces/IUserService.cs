using System.Threading.Tasks;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.Game.Services.Models;
using Upope.ServiceBase;

namespace Upope.Game.Services.Interfaces
{
    public interface IUserService: IEntityServiceBase<User>
    {
        UserParams GetUserByUserId(string userId);
        Task<TokenModel> Login(LoginModel model);
    }
}
