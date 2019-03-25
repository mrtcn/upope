using System;
using Upope.Challenge.Enums;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;

namespace Upope.Challenge.Data.Entities
{
    public interface IChallengeRequest : IEntity, IHasStatus, IOperatorFields
    {
        string ChallengeOwnerId { get; set; }
        string ChallengerId { get; set; }
        ChallengeRequestStatus ChallengeRequestStatus { get; set; }
        int ChallengeId { get; set; }
    }
    public class ChallengeRequest : IChallengeRequest
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public string ChallengeOwnerId { get; set; }
        public string ChallengerId { get; set; }
        public ChallengeRequestStatus ChallengeRequestStatus { get; set; }
        public int ChallengeId { get; set; }
        public Challenge Challenge { get; set; }
        public User Challenger { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
