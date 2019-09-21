using Newtonsoft.Json;
using System.Threading.Tasks;
using Upope.Notification.GlobalSettings;
using Upope.Notification.Services.Interfaces;
using Upope.Notification.ViewModels;
using Upope.ServiceBase.Handler;

namespace Upope.Notification.Services.Sync
{
    public class LoyaltySyncService : ILoyaltySyncService
    {
        private readonly IHttpHandler _httpHandler;
        public LoyaltySyncService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task AddCredit(CreditsViewModel model, string accessToken)
        {
            var baseUrl = AppSettingsProvider.LoyaltyBaseUrl;
            var api = AppSettingsProvider.AddCredits;

            var messageBody = JsonConvert.SerializeObject(model);
            await _httpHandler.AuthPutAsync(accessToken, baseUrl, api, messageBody);
        }

        public async Task ChargeCredit(CreditsViewModel model, string accessToken)
        {
            var baseUrl = AppSettingsProvider.LoyaltyBaseUrl;
            var api = AppSettingsProvider.ChargeCredits;

            var messageBody = JsonConvert.SerializeObject(model);
            await _httpHandler.AuthPutAsync(accessToken, baseUrl, api, messageBody);
        }
    }
}
