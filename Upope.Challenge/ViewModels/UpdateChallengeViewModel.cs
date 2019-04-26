using Upope.Challenge.Enums;

namespace Upope.Challenge.ViewModels
{
    public class UpdateChallengeInputViewModel
    {
        public int ChallengeRequestId { get; set; }
        public ChallengeRequestStatus ChallengeRequestAnswer { get; set; }
    }
}
