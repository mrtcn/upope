using Newtonsoft.Json;
using System.Threading.Tasks;
using Upope.Identity.GlobalSettings;
using Upope.Identity.Services.Interfaces;
using Upope.Identity.ViewModels;
using Upope.ServiceBase.Handler;

namespace Upope.Identity.Services.Sync
{
    public class GameUserSyncService : IGameUserSyncService
    {
        private readonly IHttpHandler _httpHandler;
        public GameUserSyncService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task<bool> SyncGameUserTable(CreateOrUpdateGameUserViewModel model, string accessToken)
        {
            var baseUrl = AppSettingsProvider.GameBaseUrl;

            var api = AppSettingsProvider.CreateOrUpdateGameUser;

            var messageBody = JsonConvert.SerializeObject(model);
            var result = await _httpHandler.AuthPostAsync<CreateOrUpdateGameUserViewModel>(accessToken, baseUrl, api, messageBody);

            if (result != null)
                return false;

            return true;
        }
    }
}
