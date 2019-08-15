using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Loyalty.EntityParams;
using Upope.ServiceBase;

namespace Upope.Loyalty.Services.Interfaces
{
    public interface ILoyaltyService : IEntityServiceBase<Data.Entities.Loyalty>
    {
        int? UserCredit(string userId);
        LoyaltyParams GetLoyaltyByUserId(string userId);
        Task<List<LoyaltyParams>> SufficientPoints(string accessToken, int point);
        void ChargeCredits(ChargeCreditsParams chargeCreditsParams);
        void AddCredits(ChargeCreditsParams chargeCreditsParams);
    }
}
