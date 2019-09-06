using System.Threading.Tasks;
using Upope.Game.GlobalSettings;
using Upope.Game.Models;
using Upope.Game.Services.Interfaces;
using Upope.ServiceBase.Handler;

namespace Upope.Game.Services.Sync
{
    public class LoyaltyService : ILoyaltyService
    {
        private readonly IHttpHandler _httpHandler;

        public LoyaltyService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }
        public async Task<LoyaltyModel> GetLoyalty(string accessToken, string userId)
        {
            var loyaltyBaseUrl = AppSettingsProvider.LoyaltyBaseUrl;
            var getLoyaltyApi = AppSettingsProvider.LoyaltyUserStats.Replace("{userId}", userId);

            var loyalty = await _httpHandler.AuthGetAsync<LoyaltyModel>(accessToken, loyaltyBaseUrl, getLoyaltyApi);
            return loyalty;
        }
    }
}
