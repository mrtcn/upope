using Upope.Challange.Data.Entities;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Models;

namespace Upope.Challange.EntityParams
{
    public class ChallengeParams: IEntityParams, IChallenge
    {
        public ChallengeParams() { }
        public ChallengeParams(Status status, string userId, int points)
        {
            Status = status;
            ChallengeOwnerId = userId;
            RewardPoint = points;
        }
        public int Id { get; set; }
        public Status Status { get; set; }
        public string ChallengeOwnerId { get; set; }
        public string ChallengerId { get; set; }
        public int RewardPoint { get; set; }
        public string WinnerId { get; set; }
    }
}
