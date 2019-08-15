using System;
using System.ComponentModel.DataAnnotations;
using Upope.Game.Enum;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.Game.Data.Entities
{
    public interface IRoundAnswer : IEntity, IHasStatus, IDateOperationFields
    {
        int GameRoundId { get; set; }
    }

    public class RoundAnswer : IRoundAnswer
    {
        [Key]
        public int Id { get; set; }
        public int GameRoundId { get; set; }
        public string UserId { get; set; }
        public Status Status { get; set; }        
        public RockPaperScissorsType Choice { get; set; }
        public GameRound GameRound { get; set; }
        public Bluff Bluff { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
