using Newtonsoft.Json;
using System.Threading.Tasks;
using Upope.Identity.GlobalSettings;
using Upope.Identity.Services.Interfaces;
using Upope.Identity.ViewModels;
using Upope.ServiceBase.Handler;

namespace Upope.Identity.Services.Sync
{
    public class ChallengeUserSyncService: IChallengeUserSyncService
    {
        private readonly IHttpHandler _httpHandler;
        public ChallengeUserSyncService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task SyncChallengeUserTable(CreateOrUpdateChallengeUserViewModel model, string accessToken)
        {
            var baseUrl = AppSettingsProvider.ChallengeBaseUrl;

            var api = AppSettingsProvider.CreateOrUpdateUser;

            var messageBody = JsonConvert.SerializeObject(model);
            var result = await _httpHandler.AuthPostAsync<CreateOrUpdateChallengeUserViewModel>(accessToken, baseUrl, api, messageBody);
        }
    }
}
