using Upope.Challenge.GlobalSettings;

namespace Upope.Challenge.Services.Models
{
    public class GetChallengerIdsModel
    {
        public GetChallengerIdsModel() { }
        public GetChallengerIdsModel(string accessToken, string baseUrl = null, string api = null)
        {
            AccessToken = accessToken;
            BaseUrl = baseUrl??AppSettingsProvider.IdentityBaseUrl;
            Api = api??"";
        }

        public string AccessToken { get; set; }
        public string BaseUrl { get; set; }
        public string Api { get; set; }
    }
}
