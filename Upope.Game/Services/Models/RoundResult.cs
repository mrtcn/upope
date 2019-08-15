
using Upope.Game.Enum;

namespace Upope.Game.EntityParams
{
    public class RoundResult
    {
        public RoundResult()
        {

        }

        public RoundResult(string winnerId, ChoiceResultType resultType, bool bluff, bool superBluff)
        {
            WinnerId = winnerId;
            ResultType = resultType;
            Bluff = bluff;
            SuperBluff = superBluff;
        }

        public string WinnerId { get; set; }
        public ChoiceResultType ResultType { get; set; }
        public bool Bluff { get; set; }
        public bool SuperBluff { get; set; }
    }
}
