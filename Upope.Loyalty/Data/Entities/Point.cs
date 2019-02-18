using System.ComponentModel.DataAnnotations;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;

namespace Upope.Loyalty.Data.Entities
{
    public interface IPoint : IEntity, IHasStatus
    {
        int Points { get; set; }
        string UserId { get; set; }
    }

    public class Point : IPoint
    {
        [Key]
        public int Id { get; set; }
        public Status Status { get; set; }
        public int Points { get; set; }
        public string UserId { get; set; }
    }
}
