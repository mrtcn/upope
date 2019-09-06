using System.Threading.Tasks;
using Upope.Game.Models;

namespace Upope.Game.Services.Interfaces
{
    public interface ILoyaltyService
    {
        Task<LoyaltyModel> GetLoyalty(string accessToken, string userId);
    }
}
