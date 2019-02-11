using System.Threading.Tasks;
using Upope.Identity.Models.GoogleResponse;
using Upope.Identity.Services.Interfaces;

namespace Upope.Identity.Services
{
    public class GoogleService : IExternalAuthService<GoogleResponse>
    {
        private readonly IExternalAuthClient _googleClient;

        public GoogleService(IExternalAuthClient googleClient)
        {
            _googleClient = googleClient;
        }

        public async Task<GoogleResponse> GetAccountAsync(string accessToken)
        {
            var result = await _googleClient.GetAsync<GoogleResponse>(
                accessToken, "v1/userinfo?scope=email");

            if (result == null)
            {
                return new GoogleResponse();
            }

            return result;
        }
    }
}
