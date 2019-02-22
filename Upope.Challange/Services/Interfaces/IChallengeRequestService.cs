using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Challange.Data.Entities;
using Upope.Challange.EntityParams;
using Upope.Challange.Services.Models;
using Upope.ServiceBase;

namespace Upope.Challange.Services.Interfaces
{
    public interface IChallengeRequestService : IEntityServiceBase<ChallengeRequest>
    {
        List<ChallengeRequestParams> ChallengeRequests(string userId);
        Task<IReadOnlyList<string>> CreateChallengeRequests(CreateChallengeRequestModel model);
        Task SetChallengeRequestsToMissed(int challengeId, int challengeRequestId);
    }
}
