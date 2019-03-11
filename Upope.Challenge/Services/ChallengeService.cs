using AutoMapper;
using ChallengeEntity = Upope.Challenge.Data.Entities.Challenge;
using Upope.Challenge.Services.Interfaces;
using Upope.ServiceBase;

namespace Upope.Challenge.Services
{
    public class ChallengeService: EntityServiceBase<ChallengeEntity>, IChallengeService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ChallengeService(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
        }
    }
}
