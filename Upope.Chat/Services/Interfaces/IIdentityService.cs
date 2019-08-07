using System.Threading.Tasks;

namespace Upope.Chat.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserId(string token, string baseUrl = null, string api = null);
    }
}
