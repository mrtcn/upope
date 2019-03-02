using Newtonsoft.Json;
using System.Threading.Tasks;
using Upope.Identity.GlobalSettings;
using Upope.Identity.Services.Interfaces;
using Upope.Identity.ViewModels;
using Upope.ServiceBase.Handler;

namespace Upope.Identity.Services.Sync
{
    public class LoyaltySyncService : ILoyaltySyncService
    {
        private readonly IHttpHandler _httpHandler;
        public LoyaltySyncService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task SyncLoyaltyTable(CreateOrUpdateLoyaltyViewModel model, string accessToken)
        {
            var baseUrl = AppSettingsProvider.LoyaltyBaseUrl;

            var api = AppSettingsProvider.CreateOrUpdateLoyalty;

            var messageBody = JsonConvert.SerializeObject(model);
            var result = await _httpHandler.AuthPostAsync<CreateOrUpdateLoyaltyViewModel>(accessToken, baseUrl, api, messageBody);
        }
    }
}
