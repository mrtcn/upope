using System.Threading.Tasks;

namespace Upope.Identity.Services.Interfaces
{
    public interface IExternalAuthClient
    {
        Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null);
        Task PostAsync(string accessToken, string endpoint, object data, string args = null);
    }
}
