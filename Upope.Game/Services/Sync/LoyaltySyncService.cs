using Newtonsoft.Json;
using System.Threading.Tasks;
using Upope.Game.GlobalSettings;
using Upope.Game.Models;
using Upope.Game.Services.Interfaces;
using Upope.ServiceBase.Handler;

namespace Upope.Game.Services.Sync
{
    public class LoyaltySyncService : ILoyaltySyncService
    {
        private readonly IHttpHandler _httpHandler;
        public LoyaltySyncService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }
        public async Task ResetWin(string userId, string accessToken)
        {
            var baseUrl = AppSettingsProvider.LoyaltyBaseUrl;
            var api = AppSettingsProvider.ResetWins.Replace("{userId}", userId);

            await _httpHandler.AuthPutAsync(accessToken, baseUrl, api);
        }

        public async Task AddWin(string userId, string accessToken)
        {
            var baseUrl = AppSettingsProvider.LoyaltyBaseUrl;
            var api = AppSettingsProvider.AddWin.Replace("{userId}", userId);

            await _httpHandler.AuthPutAsync(accessToken, baseUrl, api);
        }

        public async Task AddScores(ScoreModel model, string accessToken)
        {
            var baseUrl = AppSettingsProvider.LoyaltyBaseUrl;
            var api = AppSettingsProvider.AddScores;

            var messageBody = JsonConvert.SerializeObject(model);
            await _httpHandler.AuthPutAsync(accessToken, baseUrl, api, messageBody);
        }

        public async Task AddCredit(CreditsModel model, string accessToken)
        {
            var baseUrl = AppSettingsProvider.LoyaltyBaseUrl;
            var api = AppSettingsProvider.AddCredits;

            var messageBody = JsonConvert.SerializeObject(model);
            await _httpHandler.AuthPutAsync(accessToken, baseUrl, api, messageBody);
        }

        public async Task ChargeCredit(CreditsModel model, string accessToken)
        {
            var baseUrl = AppSettingsProvider.LoyaltyBaseUrl;
            var api = AppSettingsProvider.ChargeCredits;

            var messageBody = JsonConvert.SerializeObject(model);
            await _httpHandler.AuthPutAsync(accessToken, baseUrl, api, messageBody);
        }
    }
}
