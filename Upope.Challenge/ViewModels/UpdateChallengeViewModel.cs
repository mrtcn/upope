using Upope.Challenge.Enums;

namespace Upope.Challenge.ViewModels
{
    public class UpdateChallengeInputViewModel
    {
        public int ChallengeId { get; set; }
        public int ChallengeRequestId { get; set; }
        public ChallengeRequestStatus ChallengeRequestStatus { get; set; }
    }
}
