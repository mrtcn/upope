using System.Threading.Tasks;
using Upope.Challange.Data.Entities;
using Upope.ServiceBase;

namespace Upope.Challange.Services.Interfaces
{
    public interface IChallengeService: IEntityServiceBase<Challenge>
    {
        Task<string> GetUserId(string token, string baseUrl = null, string api = null);
    }
}
