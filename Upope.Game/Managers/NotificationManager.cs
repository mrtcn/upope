using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Upope.Game.GlobalSettings;
using Upope.Game.Interfaces;
using Upope.Game.Models;
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
            await WinInARowNotification(accessToken, userId);
        }

        private async Task WinInARowNotification(string accessToken, string userId)
        {
            var streakCount = _gameService.StreakCount(userId);

            if (streakCount % AppSettingsProvider.WinInARowModal != 0)
                return;

            var latestWinGameId = _gameService.LatestWinGameId(userId);

            var notificationBaseUrl = AppSettingsProvider.NotificationBaseUrl;
            var sendNotificationUrl = AppSettingsProvider.SendNotification;
            var notificationModel = new NotificationModel() {
                GameId = latestWinGameId,
                CreatedDate = DateTime.Now,
                NotificationType = ServiceBase.Enums.NotificationType.StreakNotification,
                UserId = userId
            };

            await _httpHandler.AuthPostAsync(accessToken, notificationBaseUrl, sendNotificationUrl, JsonConvert.SerializeObject(notificationModel));
        }
    }
}
