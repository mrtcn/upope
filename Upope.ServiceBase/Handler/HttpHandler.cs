using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Upope.ServiceBase.Models.ApiModels;

namespace Upope.ServiceBase.Handler
{
    public interface IHttpHandler
    {
        Task AuthPostAsync(string token, string baseUrl, string api, string messageBody = null);
        Task<T> AuthPostAsync<T>(string token, string baseUrl, string api, string messageBody = null) where T : class;
        Task<T> AuthGetAsync<T>(string token, string baseUrl, string api) where T : class;
        Task<T> AuthPutAsync<T>(string token, string baseUrl, string api, string messageBody = null) where T : class;
        Task AuthPutAsync(string token, string baseUrl, string api, string messageBody = null);
        Task<T> PostAsync<T>(string baseUrl, string api, string messageBody = null) where T : class;
        Task PostAsync(string baseUrl, string api, string messageBody = null);
    }
    public class HttpHandler: IHttpHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> PostAsync<T>(string baseUrl, string api, string messageBody = null) where T : class
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);

                StringContent stringContent = null;
                if (!string.IsNullOrEmpty(messageBody))
                {
                    stringContent = new StringContent(messageBody, UnicodeEncoding.UTF8, "application/json");
                }

                var result = await httpClient.PostAsync(api, stringContent);
                string resultContent = await result.Content.ReadAsStringAsync();

                var responseStatus = JsonConvert.DeserializeObject<HttpResponse>(resultContent);
                var response = JsonConvert.SerializeObject(responseStatus.Response);

                var resultObject = JsonConvert.DeserializeObject<T>(response);

                return resultObject;
            }
        }

        public async Task PostAsync(string baseUrl, string api, string messageBody = null)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);

                StringContent stringContent = null;
                if (!string.IsNullOrEmpty(messageBody))
                {
                    stringContent = new StringContent(messageBody, UnicodeEncoding.UTF8, "application/json");
                }

                var result = await httpClient.PostAsync(api, stringContent);
                string resultContent = await result.Content.ReadAsStringAsync();

                var responseStatus = JsonConvert.DeserializeObject<HttpResponse>(resultContent);
                var response = JsonConvert.SerializeObject(responseStatus.Response);

                var resultObject = JsonConvert.DeserializeObject(response);
            }
        }

        public async Task<T> AuthPostAsync<T>(string token, string baseUrl, string api, string messageBody = null) where T: class
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                StringContent stringContent = null;
                if (!string.IsNullOrEmpty(messageBody))
                {
                    stringContent = new StringContent(messageBody, UnicodeEncoding.UTF8, "application/json");
                }

                var result = await httpClient.PostAsync(api, stringContent);
                string resultContent = await result.Content.ReadAsStringAsync();

                var responseStatus = JsonConvert.DeserializeObject<HttpResponse>(resultContent);
                var response = JsonConvert.SerializeObject(responseStatus.Response);

                var resultObject = JsonConvert.DeserializeObject<T>(response);

                return resultObject;
            }
        }

        public async Task<T> AuthPutAsync<T>(string token, string baseUrl, string api, string messageBody = null) where T : class
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                StringContent stringContent = null;
                if (!string.IsNullOrEmpty(messageBody))
                {
                    stringContent = new StringContent(messageBody, UnicodeEncoding.UTF8, "application/json");
                }

                var result = await httpClient.PutAsync(api, stringContent);
                string resultContent = await result.Content.ReadAsStringAsync();

                var responseStatus = JsonConvert.DeserializeObject<HttpResponse>(resultContent);
                var response = JsonConvert.SerializeObject(responseStatus.Response);

                var resultObject = JsonConvert.DeserializeObject<T>(response);

                return resultObject;
            }
        }

        public async Task<T> AuthGetAsync<T>(string token, string baseUrl, string api) where T : class
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await httpClient.GetAsync(api);
                string resultContent = await result.Content.ReadAsStringAsync();
                var responseStatus = JsonConvert.DeserializeObject<HttpResponse>(resultContent);
                var response = JsonConvert.SerializeObject(responseStatus.Response);

                var resultObject = JsonConvert.DeserializeObject<T>(response);

                return resultObject;
            }
        }

        public async Task AuthPostAsync(string token, string baseUrl, string api, string messageBody = null)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                StringContent stringContent = null;
                if (!string.IsNullOrEmpty(messageBody))
                {
                    stringContent = new StringContent(messageBody, UnicodeEncoding.UTF8, "application/json");
                }

                var result = await httpClient.PostAsync(api, stringContent);
            }
        }

        public async Task AuthPutAsync(string token, string baseUrl, string api, string messageBody = null)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                StringContent stringContent = null;
                if (!string.IsNullOrEmpty(messageBody))
                {
                    stringContent = new StringContent(messageBody, UnicodeEncoding.UTF8, "application/json");
                }

                var result = await httpClient.PutAsync(api, stringContent);
            }
        }
    }
}
