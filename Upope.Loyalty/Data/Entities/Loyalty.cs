using System;
using System.ComponentModel.DataAnnotations;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.Loyalty.Data.Entities
{
    public interface ILoyalty : IEntity, IHasStatus, IDateOperationFields
    {
        int CurrentWinStreak { get; set; }
        int WinRecord { get; set; }
        int Credit { get; set; }
        int Score { get; set; }
        string UserId { get; set; }
    }

    public class Loyalty : ILoyalty
    {
        [Key]
        public int Id { get; set; }
        public Status Status { get; set; }
        public int CurrentWinStreak { get; set; }
        public int WinRecord { get; set; }
        public int Credit { get; set; }
        public int Score { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
