using System;
using Upope.Game.Data.Entities;
using Upope.Game.Enum;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.Models;

namespace Upope.Game.EntityParams
{
    public class BluffParams : IEntityParams, IBluff, IOperatorFields
    {
        public int Id { get; set; }
        public int GameRoundId { get; set; }
        public string UserId { get; set; }
        public bool IsSuperBluff { get; set; }
        public RockPaperScissorsType Choice { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
