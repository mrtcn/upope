using System;
using Upope.Game.Data.Entities;
using Upope.Game.Enum;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.Models;

namespace Upope.Game.EntityParams
{
    public class GameRoundParams : IEntityParams, IGameRound, IOperatorFields
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int Round { get; set; }
        public Status Status { get; set; }
        public RockPaperScissorsType HostAnswer { get; set; }
        public RockPaperScissorsType GuestAnswer { get; set; }
        public string WinnerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
