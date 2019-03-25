using System;
using System.ComponentModel.DataAnnotations;
using Upope.Game.Enum;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;

namespace Upope.Game.Data.Entities
{
    public interface IGameRound : IEntity, IHasStatus, IOperatorFields
    {
        int GameId { get; set; }
    }

    public class GameRound : IGameRound
    {
        [Key]
        public int Id { get; set; }
        public int GameId { get; set; }
        public int Round { get; set; }
        public Status Status { get; set; }        
        public RockPaperScissorsType HostAnswer { get; set; }
        public RockPaperScissorsType GuestAnswer { get; set; }
        public string WinnerId { get; set; }        
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Game Game { get; set; }
    }
}
