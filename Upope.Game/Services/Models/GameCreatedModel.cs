using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upope.Game.Services.Models
{
    public class GameCreatedModel
    {
        public int GameId { get; set; }
        public string HostUserId { get; set; }
        public string GuestUserId { get; set; }
    }
}
