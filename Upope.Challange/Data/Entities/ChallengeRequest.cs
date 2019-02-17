using Upope.Challange.Enums;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;

namespace Upope.Challange.Data.Entities
{
    public interface IChallengeRequest : IEntity, IHasStatus
    {
        int ChallengeOwnerId { get; set; }
        int? ChallengerId { get; set; }
        ChallengeRequestStatus ChallengeRequestStatus { get; set; }
    }
    public class ChallengeRequest : IChallengeRequest
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public int ChallengeOwnerId { get; set; }
        public int? ChallengerId { get; set; }
        public ChallengeRequestStatus ChallengeRequestStatus { get; set; }
        public int ChallengeId { get; set; }
        public Challenge Challenge { get; set; }
    }
}
