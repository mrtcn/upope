
using Upope.Game.Enum;

namespace Upope.Game.EntityParams
{
    public class RoundResult
    {
        public RoundResult()
        {

        }

        public RoundResult(string winnerId, RockPaperScissorsType choice, ChoiceResultType resultType, bool bluff, bool superBluff)
        {
            WinnerId = winnerId;
            Choice = choice;
            ResultType = resultType;
            Bluff = bluff;
            SuperBluff = superBluff;
        }

        public string WinnerId { get; set; }
        public RockPaperScissorsType Choice { get; set; }
        public ChoiceResultType ResultType { get; set; }
        public bool Bluff { get; set; }
        public bool SuperBluff { get; set; }
    }
}
