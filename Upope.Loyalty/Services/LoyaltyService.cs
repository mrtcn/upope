using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upope.Loyalty.EntityParams;
using Upope.Loyalty.Services.Interfaces;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;

namespace Upope.Loyalty.Services
{    
    public class LoyaltyService : EntityServiceBase<Data.Entities.Loyalty>, ILoyaltyService
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public LoyaltyService(
            ApplicationDbContext applicationDbContext,
            IIdentityService identityService,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<LoyaltyParams> GetLoyaltyByUserId(string accessToken)
        {
            var userId = await _identityService.GetUserId(accessToken);

            var loyalty = Entities.FirstOrDefault(x => x.UserId == userId && x.Status == Status.Active);
            var loyaltyParams = _mapper.Map<LoyaltyParams>(loyalty);

            return loyaltyParams;
        }

        public async Task<List<LoyaltyParams>> SufficientPoints(string accessToken, int point)
        {
            var userId = await _identityService.GetUserId(accessToken);

            var sufficientPoints = Entities
                .Where(x => x.Credit >= point 
                    && x.Status == Status.Active
                    && x.UserId != userId)
                .Take(5).ToList();

            var loyaltyParams = _mapper.Map<List<LoyaltyParams>>(sufficientPoints);

            return loyaltyParams;
        }

        public void ChargeGameCredits(ChargeGameCreditsParams chargeGameCreditsParams)
        {
            ChargeCredits(new ChargeCreditsParams(chargeGameCreditsParams.HostUserId, chargeGameCreditsParams.Credit));
            ChargeCredits(new ChargeCreditsParams(chargeGameCreditsParams.GuestUserId, chargeGameCreditsParams.Credit));
        }

        public void ChargeCredits(ChargeCreditsParams chargeCreditsParams)
        {
            var loyalty = GetLoyaltyByUserId(chargeCreditsParams.UserId);
            var loyaltyParams = _mapper.Map<LoyaltyParams>(loyalty);
            loyaltyParams.Credit -= chargeCreditsParams.Credit;

            CreateOrUpdate(loyaltyParams);
        }
    }
}
