using System.Threading.Tasks;
using Upope.Game.Services.Models;
using Upope.Game.ViewModels;

namespace Upope.Game.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserId(string token, string baseUrl = null, string api = null);
        Task<UserProfileModel> GetUserProfile(string accessToken, string id);
    }
}
