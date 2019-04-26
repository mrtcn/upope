
using Upope.Game.Enum;

namespace Upope.Game.Services.Models
{
    public class WinModel
    {
        public WinModel(string userId, bool roundWin, bool gameWin, ChoiceResultType choiceResultType)
        {
            UserId = userId;
            RoundWin = roundWin;
            GameWin = gameWin;
            ChoiceResultType = choiceResultType;
        }

        public string UserId { get; set; }
        public ChoiceResultType ChoiceResultType { get; set; }
        public bool RoundWin { get; set; }
        public bool GameWin { get; set; }
    }
}
