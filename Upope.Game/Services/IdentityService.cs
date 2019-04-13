using System.Threading.Tasks;
using Upope.Game.GlobalSettings;
using Upope.Game.Services.Interfaces;
using Upope.Game.ViewModels;
using Upope.ServiceBase.Handler;

namespace Upope.Game.Services
{
    public class IdentityService: IIdentityService
    {
        private readonly IHttpHandler _httpHandler;

        public IdentityService(
            ApplicationDbContext applicationDbContext,
            IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task<string> GetUserId(string token, string baseUrl = null, string api = null)
        {
            if (string.IsNullOrEmpty(baseUrl))
                baseUrl = AppSettingsProvider.IdentityBaseUrl;

            if (string.IsNullOrEmpty(api))
                api = AppSettingsProvider.GetUserId;

            var userId = await _httpHandler.AuthPostAsync<string>(token, baseUrl, api);

            return userId;
        }

        public async Task<UserProfileModel> GetUserProfile(string accessToken, string id)
        {
            var baseUrl = AppSettingsProvider.IdentityBaseUrl;
            var apiUrl = AppSettingsProvider.GetUserProfileUrl;

            var userProfile = await _httpHandler.AuthPostAsync<UserProfileModel>(accessToken, baseUrl, apiUrl);

            return userProfile;
        }
    }
}
