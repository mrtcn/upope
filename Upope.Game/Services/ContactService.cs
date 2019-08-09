using System.Threading.Tasks;
using Upope.Game.Services.Interfaces;
using Upope.ServiceBase.GlobalSettings;
using Upope.ServiceBase.Handler;

namespace Upope.Game.Services
{
    public class ContactService : IContactService
    {
        private readonly IHttpHandler _httpHandler;
        public ContactService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }
        public async Task SyncContactTable(string accessToken, string userId, string contactUserId)
        {
            var baseUrl = AppSettingsProvider.ChatBaseUrl;

            var api = AppSettingsProvider.CreateContact.Replace("{contactUserId}", userId);

            await _httpHandler.AuthPostAsync(accessToken, baseUrl, api, null);
        }
    }
}
