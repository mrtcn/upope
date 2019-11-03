using System;
using Upope.Challenge.Data.Entities;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.Models;

namespace Upope.Challenge.EntityParams
{
    public class ChallengeParams: IEntityParams, IChallenge, IOperatorFields
    {
        public ChallengeParams() { }
        public ChallengeParams(Status status, string userId, int points)
        {
            Status = status;
            ChallengeOwnerId = userId;
            RewardPoint = points;
        }

        public ChallengeParams(Status status, string userId, int points, bool isBotActivated)
        {
            Status = status;
            ChallengeOwnerId = userId;
            RewardPoint = points;
            IsBotActivated = isBotActivated;
        }

        public int Id { get; set; }
        public Status Status { get; set; }
        public string ChallengeOwnerId { get; set; }
        public string ChallengerId { get; set; }
        public int RewardPoint { get; set; }
        public string WinnerId { get; set; }
        public bool IsBotActivated { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
