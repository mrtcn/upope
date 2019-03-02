using System.Threading.Tasks;

namespace Upope.Loyalty.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserId(string token, string baseUrl = null, string api = null);
    }
}
