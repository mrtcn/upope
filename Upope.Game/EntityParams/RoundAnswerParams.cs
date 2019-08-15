using System;
using Upope.Game.Data.Entities;
using Upope.Game.Enum;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.Models;

namespace Upope.Game.EntityParams
{
    public class RoundAnswerParams : IEntityParams, IRoundAnswer, IOperatorFields
    {
        public int Id { get; set; }
        public int GameRoundId { get; set; }
        public string UserId { get; set; }
        public Status Status { get; set; }
        public RockPaperScissorsType Choice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
