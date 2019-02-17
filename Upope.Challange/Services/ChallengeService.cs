using AutoMapper;
using Upope.Challange.Data.Entities;
using Upope.Challange.Services.Interfaces;
using Upope.ServiceBase;

namespace Upope.Challange.Services
{
    public class ChallengeService: EntityServiceBase<Challenge>, IChallengeService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public ChallengeService(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }
    }
}
