﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Loyalty.EntityParams;
using Upope.ServiceBase;

namespace Upope.Loyalty.Services.Interfaces
{
    public interface ILoyaltyService : IEntityServiceBase<Data.Entities.Loyalty>
    {
        LoyaltyParams GetLoyaltyByUserId(string userId);
        List<LoyaltyParams> SufficientPoints(int point);
    }
}
