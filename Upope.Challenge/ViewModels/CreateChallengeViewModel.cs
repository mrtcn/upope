using Upope.ServiceBase.Enums;

namespace Upope.Challenge.ViewModels
{
    public class CreateChallengeViewModel
    {
        public int BetAmount { get; set; }
        public int Range { get; set; }
        public Gender Sex { get; set; }
        public bool IsBotActivated { get; set; }
    }
}
