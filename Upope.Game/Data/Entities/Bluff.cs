using System;
using System.ComponentModel.DataAnnotations;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.Game.Data.Entities
{
    public interface IBluff: IEntity, IHasStatus, IDateOperationFields
    {
        int GameRoundId { get; set; }
        int GameId { get; set; }
        string UserId { get; set; }
        bool IsSuperBluff { get; set; }
    }

    public class Bluff: IBluff
    {
        [Key]
        public int Id { get; set; }
        public int GameRoundId { get; set; }
        public int GameId { get; set; }
        public string UserId { get; set; }
        public bool IsSuperBluff { get; set; }
        public Status Status { get; set; }
        public User User { get; set; }
        public GameRound GameRound { get; set; }
        public Game Game { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
