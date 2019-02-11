namespace Upope.Identity.Models
{
    public class TokenModel
    {
        public TokenModel(string accessToken)
        {
            AccessToken = accessToken;
        }

        public string AccessToken { get; set; }
    }
}
