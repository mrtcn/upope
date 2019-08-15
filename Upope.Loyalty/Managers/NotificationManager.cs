using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Upope.Loyalty.GlobalSettings;
using Upope.Loyalty.Interfaces;
using Upope.Loyalty.Models;
using Upope.Loyalty.Services.Interfaces;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Handler;

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
        }

        private async Task WatchAdNotification(string accessToken, string userId)
        {
            await SendCreditWarningNotifications(accessToken, userId, AppSettingsProvider.WatchAdCreditLimit, NotificationType.WatchAdNotification);
        }

        private async Task UpgradeToProNotification(string accessToken, string userId)
        {
            await SendCreditWarningNotifications(accessToken, userId, AppSettingsProvider.UpgradeToProCreditLimit, NotificationType.UpgradeToProNotification);
        }

        private async Task SendCreditWarningNotifications(string accessToken, string userId, int creditLimit, NotificationType notificationType)
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
                UserId = userId
            };

            await _httpHandler.AuthPostAsync(accessToken, notificationBaseUrl, sendNotificationUrl, JsonConvert.SerializeObject(notificationModel));
        }
    }
}
