
namespace Upope.Challenge.GlobalSettings
{
    public static class AppSettingsProvider
    {
        public static string IdentityBaseUrl { get; set; }
        public static string GetUserId { get; set; }
        public static string GetUserProfile { get; set; }
        public static string Login { get; set; }


        public static string ChallengeBaseUrl { get; set; }
        public static string UpdateChallenge { get; set; }

        public static string LoyaltyBaseUrl { get; set; }
        public static string SufficientPointsUrl { get; set; }
        public static string FilterUsersUrl { get; set; }

        public static string GameBaseUrl { get; set; }
        public static string CreateGameUrl { get; set; }
        public static string AccessToken { get; set; }
    }
}
