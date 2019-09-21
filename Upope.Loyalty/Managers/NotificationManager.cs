using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Upope.Loyalty.GlobalSettings;
using Upope.Loyalty.Interfaces;
using Upope.Loyalty.Services.Interfaces;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Handler;
using Upope.ServiceBase.Models;

namespace Upope.Loyalty.Managers
{
    public class NotificationManager : INotificationManager
    {
        private readonly ILoyaltyService _loyaltyService;
        private readonly IHttpHandler _httpHandler;

        public NotificationManager(
            ILoyaltyService loyaltyService,
            IHttpHandler httpHandler)
        {
            _loyaltyService = loyaltyService;
            _httpHandler = httpHandler;
        }

        public async Task SendNotification(string accessToken, string userId)
        {
            await WatchAdNotification(accessToken, userId);
            await UpgradeToProNotification(accessToken, userId);
            await WinInARowNotification(accessToken, userId);
        }

        private async Task WatchAdNotification(string accessToken, string userId)
        {
            await SendCreditWarningNotifications(accessToken, userId, AppSettingsProvider.WatchAdCreditLimit, AppSettingsProvider.WatchingAdCreditReward, NotificationType.WatchAdNotification);
        }

        private async Task UpgradeToProNotification(string accessToken, string userId)
        {
            await SendCreditWarningNotifications(accessToken, userId, AppSettingsProvider.UpgradeToProCreditLimit, AppSettingsProvider.UpgradeToProCreditReward, NotificationType.UpgradeToProNotification);
        }

        private async Task SendCreditWarningNotifications(string accessToken, string userId, int creditLimit, int creditToEarn, NotificationType notificationType)
        {
            var credit = _loyaltyService.UserCredit(userId);

            if (credit > creditLimit)
                return;

            var notificationBaseUrl = AppSettingsProvider.NotificationBaseUrl;
            var sendNotificationUrl = AppSettingsProvider.SendNotification;
            var notificationModel = new NotificationModel()
            {
                GameId = 0,
                CreatedDate = DateTime.Now,
                NotificationType = notificationType,
                UserId = userId,
                UserCredits = credit.GetValueOrDefault(),
                CreditsToEarn = creditToEarn,
                WinStreakCount = null,
                ImagePath = null,
                IsActionTaken = false,
            };

            await _httpHandler.AuthPostAsync(accessToken, notificationBaseUrl, sendNotificationUrl, JsonConvert.SerializeObject(notificationModel));
        }

        private async Task WinInARowNotification(string accessToken, string userId)
        {
            var loyalty = _loyaltyService.GetLoyaltyByUserId(userId);

            var streakCount = loyalty.CurrentWinStreak;

            if (streakCount % AppSettingsProvider.WinInARowModal != 0)
                return;

            var notificationBaseUrl = AppSettingsProvider.NotificationBaseUrl;
            var sendNotificationUrl = AppSettingsProvider.SendNotification;
            var notificationModel = new NotificationModel()
            {
                CreatedDate = DateTime.Now,
                NotificationType = ServiceBase.Enums.NotificationType.StreakNotification,
                UserId = userId,
                CreditsToEarn = streakCount * AppSettingsProvider.WinInARowFactor,
                WinStreakCount = streakCount
            };

            await _httpHandler.AuthPostAsync(accessToken, notificationBaseUrl, sendNotificationUrl, JsonConvert.SerializeObject(notificationModel));
        }
    }
}
