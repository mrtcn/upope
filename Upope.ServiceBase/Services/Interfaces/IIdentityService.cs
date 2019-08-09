using System.Threading.Tasks;
using Upope.ServiceBase.Models;

namespace Upope.ServiceBase.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserId(string token, string baseUrl = null, string api = null);
        Task<UserProfile> GetUserProfileByAccessToken(string token, string baseUrl = null, string api = null);
        Task<UserProfile> GetUserProfileById(string token, string userId, string baseUrl = null, string api = null);
    }
}
