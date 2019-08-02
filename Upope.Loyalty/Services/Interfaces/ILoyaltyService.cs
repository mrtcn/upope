using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Loyalty.EntityParams;
using Upope.ServiceBase;

namespace Upope.Loyalty.Services.Interfaces
{
    public interface ILoyaltyService : IEntityServiceBase<Data.Entities.Loyalty>
    {
        Task<LoyaltyParams> GetLoyaltyByUserId(string accessToken);
        Task<List<LoyaltyParams>> SufficientPoints(string accessToken, int point);
        void ChargeCredits(ChargeCreditsParams chargeCreditsParams);
        void ChargeGameCredits(ChargeGameCreditsParams chargeGameCreditsParams);
    }
}
