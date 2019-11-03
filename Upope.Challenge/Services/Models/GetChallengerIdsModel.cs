using Upope.Challenge.GlobalSettings;
using Upope.Challenge.ViewModels;

namespace Upope.Challenge.Services.Models
{
    public class GetChallengerIdsModel
    {
        public GetChallengerIdsModel() { }
        public GetChallengerIdsModel(string accessToken, FilterUsersViewModel model, string baseUrl = null, string api = null)
        {
            AccessToken = accessToken;
            BaseUrl = baseUrl??AppSettingsProvider.IdentityBaseUrl;
            Api = api??"";
            FilterUsersViewModel = model;
        }

        public string AccessToken { get; set; }
        public string BaseUrl { get; set; }
        public string Api { get; set; }
        public FilterUsersViewModel FilterUsersViewModel { get; set; }
    }
}
