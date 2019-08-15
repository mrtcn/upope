
using Upope.Game.Enum;

namespace Upope.Game.Services.Models
{
    public class WinModel
    {
        public WinModel(string userId, ChoiceResultType choiceResultType)
        {
            UserId = userId;
            ChoiceResultType = choiceResultType;
        }

        public string UserId { get; set; }
        public ChoiceResultType ChoiceResultType { get; set; }
    }
}
