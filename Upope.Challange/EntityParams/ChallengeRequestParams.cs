using Upope.Challange.Data.Entities;
using Upope.Challange.Enums;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Models;

namespace Upope.Challange.EntityParams
{
    public class ChallengeRequestParams : IEntityParams, IChallengeRequest
    {
        public ChallengeRequestParams() { }
        public ChallengeRequestParams(Status status)
        {
            Status = status;
        }
        public int Id { get; set; }
        public Status Status { get; set; }
        public int ChallengeOwnerId { get; set; }
        public int? ChallengerId { get; set; }
        public ChallengeRequestStatus ChallengeRequestStatus { get; set; }
    }
}
