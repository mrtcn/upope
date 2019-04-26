using System;
using System.Collections.Generic;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.Challenge.Data.Entities
{
    public interface IChallenge : IEntity, IHasStatus, IDateOperationFields
    {
        string ChallengeOwnerId { get; set; }
        string ChallengerId { get; set; }
        int RewardPoint { get; set; }
        string WinnerId { get; set; }
    }
    public class Challenge: IChallenge
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public string ChallengeOwnerId { get; set; }
        public string ChallengerId { get; set; }
        public int RewardPoint { get; set; }
        public string  WinnerId { get; set; }
        public List<ChallengeRequest> ChallengeRequests { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
