using Upope.Loyalty.Data.Entities;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Models;

namespace Upope.Loyalty.EntityParams
{
    public class PointParams: IEntityParams, IPoint
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public int Points { get; set; }
        public string UserId { get; set; }
    }
}
