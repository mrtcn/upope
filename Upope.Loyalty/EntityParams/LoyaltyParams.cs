using Upope.Loyalty.Data.Entities;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Models;

namespace Upope.Loyalty.EntityParams
{
    public class LoyaltyParams: IEntityParams, ILoyalty
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public int Win { get; set; }
        public int Credit { get; set; }
        public int Score { get; set; }
        public string UserId { get; set; }
    }
}
