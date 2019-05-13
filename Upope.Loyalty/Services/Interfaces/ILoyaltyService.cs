using System.Collections.Generic;
using Upope.Loyalty.EntityParams;
using Upope.ServiceBase;

namespace Upope.Loyalty.Services.Interfaces
{
    public interface ILoyaltyService : IEntityServiceBase<Data.Entities.Loyalty>
    {
        LoyaltyParams GetLoyaltyByUserId(string userId);
        List<LoyaltyParams> SufficientPoints(string userId, int point);
        void ChargeCredits(ChargeCreditsParams chargeCreditsParams);
        void ChargeGameCredits(ChargeGameCreditsParams chargeGameCreditsParams);
    }
}
