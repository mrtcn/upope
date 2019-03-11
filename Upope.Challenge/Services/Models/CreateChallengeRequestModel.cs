
namespace Upope.Challenge.Services.Models
{
    public class CreateChallengeRequestModel
    {
        public CreateChallengeRequestModel() { }
        public CreateChallengeRequestModel(string accessToken, int challengeId, string challengeOwnerId, int points) {
            AccessToken = accessToken;
            ChallengeId = challengeId;
            ChallengeOwnerId = challengeOwnerId;
            Points = points;
        }
        public string AccessToken { get; set; }
        public int ChallengeId { get; set; }
        public string ChallengeOwnerId { get; set; }
        public int Points { get; set; }
    }
}
