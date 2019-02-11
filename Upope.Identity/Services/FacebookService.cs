using System.Threading.Tasks;
using Upope.Identity.Models.FacebookResponse;
using Upope.Identity.Services.Interfaces;

namespace Upope.Identity.Services
{
    public class FacebookService : IExternalAuthService<FacebookResponse>
    {
        private readonly IExternalAuthClient _facebookClient;

        public FacebookService(IExternalAuthClient facebookClient)
        {
            _facebookClient = facebookClient;
        }

        public async Task<FacebookResponse> GetAccountAsync(string accessToken)
        {
            var result = await _facebookClient.GetAsync<FacebookResponse>(
                accessToken, "me", "fields=id,name,email,first_name,last_name,age_range,birthday,gender,locale,picture");

            if (result == null)
            {
                return new FacebookResponse();
            }

            return result;
        }
    }
}
