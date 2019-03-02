using AutoMapper;
using System.Threading.Tasks;
using Upope.Challange.GlobalSettings;
using Upope.Challange.Services.Interfaces;
using Upope.ServiceBase.Handler;

namespace Upope.Challange.Services
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
    }
}
