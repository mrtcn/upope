using Upope.Challange.Data.Entities;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Models;

namespace Upope.Challange.EntityParams
{
    public class ChallengeParams: IEntityParams, IChallenge
    {
        public ChallengeParams() { }
        public ChallengeParams(Status status)
        {
            Status = status;
        }
        public int Id { get; set; }
        public Status Status { get; set; }
        public int ChallengeOwnerId { get; set; }
        public int? ChallengerId { get; set; }
        public int RewardPoint { get; set; }
        public int? WinnerId { get; set; }
    }
}
