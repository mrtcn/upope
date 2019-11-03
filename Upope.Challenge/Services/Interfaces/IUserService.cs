using System.Threading.Tasks;
using Upope.Challenge.Data.Entities;
using Upope.Challenge.EntityParams;
using Upope.Challenge.Services.Models;
using Upope.ServiceBase;

namespace Upope.Challenge.Services.Interfaces
{
    public interface IUserService: IEntityServiceBase<User>
    {
        UserParams GetUserByUserId(string userId);
        Task<TokenModel> Login(LoginModel model);
    }
}
