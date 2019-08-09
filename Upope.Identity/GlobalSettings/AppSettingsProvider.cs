
namespace Upope.Identity.GlobalSettings
{
    public static class AppSettingsProvider
    {
        public static string LoyaltyBaseUrl { get; set; }
        public static string CreateOrUpdateLoyalty { get; set; }

        public static string ChallengeBaseUrl { get; set; }
        public static string CreateOrUpdateUser { get; set; }

        public static string GameBaseUrl { get; set; }
        public static string CreateOrUpdateGameUser { get; set; }
    }
}
