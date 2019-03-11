
namespace Upope.Challenge.Services.Models
{
    public class CreateChallengeRequestForUserModel
    {
        public CreateChallengeRequestForUserModel() { }
        public CreateChallengeRequestForUserModel(string accessToken, int challengeId, string challengeOwnerId, string challengerId, int points) {
            AccessToken = accessToken;
            ChallengeId = challengeId;
            ChallengeOwnerId = challengeOwnerId;
            ChallengerId = challengerId;
            Points = points;
        }
        public string AccessToken { get; set; }
        public int ChallengeId { get; set; }
        public string ChallengeOwnerId { get; set; }
        public string ChallengerId { get; set; }
        public int Points { get; set; }
    }
}
