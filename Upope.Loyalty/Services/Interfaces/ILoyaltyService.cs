using System.Collections.Generic;
using Upope.Loyalty.Data.Entities;
using Upope.Loyalty.EntityParams;
using Upope.ServiceBase;

namespace Upope.Loyalty.Services.Interfaces
{
    public interface ILoyaltyService : IEntityServiceBase<Point>
    {
        PointParams GetPointByUserId(string userId);
        List<PointParams> GetSufficientPoints(int point);
    }
}
