
using Upope.Game.Enum;

namespace Upope.Game.EntityParams
{
    public class SendChoiceParams
    {
        public string AccessToken { get; set; }
        public int GameId { get; set; }
        public string UserId { get; set; }
        public RockPaperScissorsType Choice { get; set; }
    }
}
