using System.Threading.Tasks;
using Upope.Chat.GlobalSettings;
using Upope.Chat.Services.Interfaces;
using Upope.ServiceBase.Handler;

namespace Upope.Chat.Services
{
    public class IdentityService: IIdentityService
    {
        private readonly IHttpHandler _httpHandler;

        public IdentityService(
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

            var userId = await _httpHandler.AuthGetAsync<string>(token, baseUrl, api);

            return userId;
        }
    }
}
