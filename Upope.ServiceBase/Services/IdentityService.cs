using System.Threading.Tasks;
using Upope.ServiceBase.GlobalSettings;
using Upope.ServiceBase.Handler;
using Upope.ServiceBase.Models;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.ServiceBase.Services
{
    public class IdentityService: IIdentityService
    {
        private readonly IHttpHandler _httpHandler;

        public IdentityService(
            IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task<UserProfile> GetUserProfileByAccessToken(string token, string baseUrl = null, string api = null)
        {
            if (string.IsNullOrEmpty(baseUrl))
                baseUrl = AppSettingsProvider.IdentityBaseUrl;

            if (string.IsNullOrEmpty(api))
                api = AppSettingsProvider.GetUserProfile;

            var userId = await _httpHandler.AuthGetAsync<UserProfile>(token, baseUrl, api);

            return userId;
        }

        public async Task<string> GetUserId(string token, string baseUrl = null, string api = null)
        {
            if (string.IsNullOrEmpty(baseUrl))
                baseUrl = AppSettingsProvider.IdentityBaseUrl;

            if (string.IsNullOrEmpty(api))
                api = AppSettingsProvider.GetUserId;

            var user = await _httpHandler.AuthGetAsync<string>(token, baseUrl, api);

            return user;
        }

        public async Task<UserProfile> GetUserProfileById(string token, string userId, string baseUrl = null, string api = null)
        {
            if (string.IsNullOrEmpty(baseUrl))
                baseUrl = AppSettingsProvider.IdentityBaseUrl;

            if (string.IsNullOrEmpty(api))
                api = AppSettingsProvider.GetUserProfileById.Replace("{id}", userId);

            var user = await _httpHandler.AuthGetAsync<UserProfile>(token, baseUrl, api);

            return user;
        }
    }
}
