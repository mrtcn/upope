using Upope.Challenge.Data.Entities;
using Upope.Challenge.Enums;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Models;

namespace Upope.Challenge.EntityParams
{
    public class ChallengeRequestParams : IEntityParams, IChallengeRequest
    {
        public ChallengeRequestParams() { }
        public ChallengeRequestParams(Status status)
        {
            Status = status;
        }

        public ChallengeRequestParams(
            Status status, 
            string challengeOwnerId, 
            string challengerId, 
            int challengeId,
            ChallengeRequestStatus challengeRequestStatus)
        {
            Status = status;
            ChallengeOwnerId = challengeOwnerId;
            ChallengerId = challengerId;
            ChallengeId = challengeId;
            ChallengeRequestStatus = challengeRequestStatus;
        }

        public int Id { get; set; }
        public Status Status { get; set; }
        public string ChallengeOwnerId { get; set; }
        public string ChallengerId { get; set; }
        public int ChallengeId { get; set; }
        public ChallengeRequestStatus ChallengeRequestStatus { get; set; }
    }    
}
