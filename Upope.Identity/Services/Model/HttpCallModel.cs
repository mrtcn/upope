using Upope.Identity.GlobalSettings;

namespace Upope.Identity.Services.Model
{
    public class HttpCallModel
    {
        public HttpCallModel() { }
        public HttpCallModel(string accessToken, string baseUrl = null, string api = null, string messageBody = null)
        {
            AccessToken = accessToken;
            BaseUrl = baseUrl ?? AppSettingsProvider.IdentityBaseUrl;
            Api = api ?? "";
            MessageBody = messageBody;
        }

        public string AccessToken { get; set; }
        public string BaseUrl { get; set; }
        public string Api { get; set; }
        public string MessageBody { get; set; }
    }
}
