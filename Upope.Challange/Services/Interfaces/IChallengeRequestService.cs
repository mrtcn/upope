using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Challange.Data.Entities;
using Upope.Challange.Services.Models;
using Upope.ServiceBase;

namespace Upope.Challange.Services.Interfaces
{
    public interface IChallengeRequestService : IEntityServiceBase<ChallengeRequest>
    {
        List<ChallengeRequestModel> ChallengeRequests(string userId);
        Task<IReadOnlyList<string>> CreateChallengeRequests(CreateChallengeRequestModel model);
    }
}
