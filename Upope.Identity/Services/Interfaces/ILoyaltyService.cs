using System.Threading.Tasks;
using Upope.Identity.Models;

namespace Upope.Identity.Services.Interfaces
{
    public interface ILoyaltyService
    {
        Task<LoyaltyModel> GetLoyalty(string accessToken, string userId);
    }
}
