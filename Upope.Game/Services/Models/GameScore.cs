using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upope.Game.Services.Models
{
    public class GameScore
    {
        public string UserId { get; set; }
        public int WinAmount { get; set; }
        public int LoseAmount { get; set; }
        public int BluffAmount { get; set; }
        public int BluffPoints { get; set; }
        public int SuperBluffAmount { get; set; }
        public int SuperBluffPoints { get; set; }
        public int DefeatlessPoints { get; set; }
        public int TotalPoints { get; set; }
        public int GainedCredits { get; set; }
    }
}
