using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Upope.Identity.Services.Interfaces;

namespace Upope.Identity.Services
{
    public class GoogleClient : IExternalAuthClient
    {
        private readonly HttpClient _httpClient;

        public GoogleClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://www.googleapis.com/oauth2/v3/")
            };
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync($"{endpoint}");
            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task PostAsync(string accessToken, string endpoint, object data, string args = null)
        {
            var payload = GetPayload(data);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            await _httpClient.PostAsync($"{endpoint}", payload);
        }

        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
