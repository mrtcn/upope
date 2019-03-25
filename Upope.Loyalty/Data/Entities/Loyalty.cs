using System;
using System.ComponentModel.DataAnnotations;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;

namespace Upope.Loyalty.Data.Entities
{
    public interface ILoyalty : IEntity, IHasStatus, IOperatorFields
    {
        int Win { get; set; }
        int Credit { get; set; }
        int Score { get; set; }
        string UserId { get; set; }
    }

    public class Loyalty : ILoyalty
    {
        [Key]
        public int Id { get; set; }
        public Status Status { get; set; }
        public int Win { get; set; }
        public int Credit { get; set; }
        public int Score { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
