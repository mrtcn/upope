using System.Threading.Tasks;
using Upope.Challenge.Models;

namespace Upope.Challenge.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserId(string token, string baseUrl = null, string api = null);
        Task<UserProfile> GetUserProfile(string token, string baseUrl = null, string api = null);
    }
}
