﻿using System.Collections.Generic;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;

namespace Upope.Challange.Data.Entities
{
    public interface IChallenge : IEntity, IHasStatus
    {
        int ChallengeOwnerId { get; set; }
        int? ChallengerId { get; set; }
        int RewardPoint { get; set; }
        int? WinnerId { get; set; }
    }
    public class Challenge: IChallenge
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public int ChallengeOwnerId { get; set; }
        public int? ChallengerId { get; set; }
        public int RewardPoint { get; set; }
        public int?  WinnerId { get; set; }
        public List<ChallengeRequest> ChallengeRequests { get; set; }
    }
}
