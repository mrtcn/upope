using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Upope.Loyalty.EntityParams;
using Upope.Loyalty.Services.Interfaces;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Handler;

namespace Upope.Loyalty.Services
{    
    public class LoyaltyService : EntityServiceBase<Data.Entities.Loyalty>, ILoyaltyService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public LoyaltyService(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public LoyaltyParams GetLoyaltyByUserId(string userId)
        {
            var loyalty = Entities.FirstOrDefault(x => x.UserId == userId && x.Status == Status.Active);
            var loyaltyParams = _mapper.Map<LoyaltyParams>(loyalty);

            return loyaltyParams;
        }

        public List<LoyaltyParams> SufficientPoints(int point)
        {
            var sufficientPoints = Entities
                .Where(x => x.Credit >= point && x.Status == Status.Active)
                .Take(5).ToList();

            var loyaltyParams = _mapper.Map<List<LoyaltyParams>>(sufficientPoints);

            return loyaltyParams;
        }
    }
}
