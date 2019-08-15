using Upope.Game.Enum;

namespace Upope.Game.ViewModels
{
    public class SendChoiceViewModel
    {
        public int GameId { get; set; }
        public int GameRoundId { get; set; }
        public int Round { get; set; }
        public RockPaperScissorsType Choice { get; set; }
    }
}
