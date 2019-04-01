using System;
using Upope.Game.Data.Entities;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.Models;

namespace Upope.Game.EntityParams
{
    public class GameParams: IEntityParams, IGame, IOperatorFields
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public string HostUserId { get; set; }
        public string GuestUserId { get; set; }
        public string WinnerId { get; set; }
        public int Credit { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
