using Newtonsoft.Json;
using System.Threading.Tasks;
using Upope.Challenge.GlobalSettings;
using Upope.Challenge.ViewModels;
using Upope.Game.Services.Interfaces;
using Upope.ServiceBase.Handler;

namespace Upope.Challenge.Services.Sync
{
    public class GameSyncService : IGameSyncService
    {
        private readonly IHttpHandler _httpHandler;
        public GameSyncService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task SyncGameTable(CreateOrUpdateGameViewModel model, string accessToken)
        {
            var baseUrl = AppSettingsProvider.GameBaseUrl;

            var api = AppSettingsProvider.CreateGameUrl;

            var messageBody = JsonConvert.SerializeObject(model);
            var result = await _httpHandler.AuthPostAsync<CreateOrUpdateGameViewModel>(accessToken, baseUrl, api, messageBody);
        }
    }
}
