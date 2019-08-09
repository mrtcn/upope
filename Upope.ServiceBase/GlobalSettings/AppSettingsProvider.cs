
using Upope.ServiceBase.Helpers;

namespace Upope.ServiceBase.GlobalSettings
{
    public static class AppSettingsProvider
    {
        public static string IdentityBaseUrl { get; set; } = ConfigurationHelper.GetConfig().GetSection("Upope.Identity:BaseUrl").Value;
        public static string GetUserId { get; set; } = ConfigurationHelper.GetConfig().GetSection("Upope.Identity:GetUserId").Value;
        public static string GetUserProfile { get; set; } = ConfigurationHelper.GetConfig().GetSection("Upope.Identity:GetUserProfile").Value;
        public static string GetUserProfileById { get; set; } = ConfigurationHelper.GetConfig().GetSection("Upope.Identity:GetUserProfileById").Value;
        public static string ChatBaseUrl { get; set; } = ConfigurationHelper.GetConfig().GetSection("Upope.Chat:BaseUrl").Value;
        public static string CreateContact { get; set; } = ConfigurationHelper.GetConfig().GetSection("Upope.Chat:CreateContact").Value;
    }
}
