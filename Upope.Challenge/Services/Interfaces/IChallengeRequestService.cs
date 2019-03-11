using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Challenge.Data.Entities;
using Upope.Challenge.Services.Models;
using Upope.ServiceBase;

namespace Upope.Challenge.Services.Interfaces
{
    public interface IChallengeRequestService : IEntityServiceBase<ChallengeRequest>
    {
        List<ChallengeRequestModel> ChallengeRequests(string userId);
        Task<IReadOnlyList<string>> CreateChallengeRequests(CreateChallengeRequestModel model);
        Task RejectAcceptChallenge(RejectAcceptChallengeModel model);
    }
}
