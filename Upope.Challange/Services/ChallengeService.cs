using Upope.Challange.Data.Entities;
using Upope.Challange.Services.Interfaces;
using Upope.ServiceBase;

namespace Upope.Challange.Services
{
    public class ChallengeService: EntityServiceBase<Challenge>, IChallengeService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ChallengeService(ApplicationDbContext applicationDbContext): base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
    }
}
