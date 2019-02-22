using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Loyalty.Data.Entities;
using Upope.Loyalty.EntityParams;
using Upope.ServiceBase;

namespace Upope.Loyalty.Services.Interfaces
{
    public interface ILoyaltyService : IEntityServiceBase<Point>
    {
        PointParams GetPointByUserId(string userId);
        List<PointParams> SufficientPoints(int point);
        Task<string> GetUserId(string token, string baseUrl = null, string api = null);
    }
}
