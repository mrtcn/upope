using System.Threading.Tasks;
using Upope.Game.Interfaces;
using Upope.Game.Services.Interfaces;
using Upope.ServiceBase.Handler;

namespace Upope.Game.Managers
{
    public class NotificationManager : INotificationManager
    {
        private readonly IGameService _gameService;
        private readonly IHttpHandler _httpHandler;

        public NotificationManager(
            IGameService gameService,
            IHttpHandler httpHandler)
        {
            _gameService = gameService;
            _httpHandler = httpHandler;
        }
        public async Task SendNotification(string accessToken, string userId)
        {
        }
    }
}
