
namespace Upope.Loyalty.GlobalSettings
{
    public static class AppSettingsProvider
    {
        public static string IdentityBaseUrl { get; set; }
        public static string GetUserId { get; set; }
        public static string NotificationBaseUrl { get; set; }
        public static string SendNotification { get; set; }
        public static int WatchAdCreditLimit { get; set; }
        public static int UpgradeToProCreditLimit { get; set; }
    }
}
