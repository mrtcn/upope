
using Upope.ServiceBase.Enums;

namespace Upope.Challenge.Services.Models
{
    public class CreateChallengeRequestModel
    {
        public CreateChallengeRequestModel() { }
        public CreateChallengeRequestModel(
            string accessToken, 
            int challengeId, 
            string challengeOwnerId, 
            int points,
            int range,
            Gender gender) {
            AccessToken = accessToken;
            ChallengeId = challengeId;
            ChallengeOwnerId = challengeOwnerId;
            Points = points;
            Range = range;
            Gender = gender;
        }
        public string AccessToken { get; set; }
        public int ChallengeId { get; set; }
        public string ChallengeOwnerId { get; set; }
        public int Points { get; set; }
        public int Range { get; set; }
        public Gender Gender { get; set; }
    }
}
