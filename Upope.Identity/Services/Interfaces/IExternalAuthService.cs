using System.Threading.Tasks;

namespace Upope.Identity.Services.Interfaces
{
    public interface IExternalAuthService<T>
    {
        Task<T> GetAccountAsync(string accessToken);
    }
}
