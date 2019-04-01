using Newtonsoft.Json;
using System.Threading.Tasks;
using Upope.Challenge.GlobalSettings;
using Upope.Challenge.Services.Interfaces;
using Upope.Challenge.ViewModels;
using Upope.ServiceBase.Handler;

namespace Upope.Challenge.Services
{
    public class GameService : IGameService
    {
        private readonly IHttpHandler _httpHandler;
        public GameService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task CreateGame(CreateGameModel model, string accessToken)
        {
            var baseUrl = AppSettingsProvider.GameBaseUrl;

            var api = AppSettingsProvider.CreateGameUrl;

            var messageBody = JsonConvert.SerializeObject(model);
            var result = await _httpHandler.AuthPostAsync<CreateGameModel>(accessToken, baseUrl, api, messageBody);
        }
    }
}
