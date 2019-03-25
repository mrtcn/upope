using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;

namespace Upope.Game.Data.Entities
{
    public interface IGame : IEntity, IHasStatus, IOperatorFields
    {
        string HostUserId { get; set; }
        string GuestUserId { get; set; }
    }

    public class Game : IGame
    {
        [Key]
        public int Id { get; set; }
        public Status Status { get; set; }
        public string HostUserId { get; set; }
        public string GuestUserId { get; set; }
        public string WinnerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public List<GameRound> GameRounds { get; set; }
    }
}
