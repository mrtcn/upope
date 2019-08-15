using Upope.Game.Enum;

namespace Upope.Game.ViewModels
{
    public class SendBluffViewModel
    {
        public int GameId { get; set; }
        public int GameRoundId { get; set; }
        public int Round { get; set; }
        public RockPaperScissorsType Choice { get; set; }
    }
}
