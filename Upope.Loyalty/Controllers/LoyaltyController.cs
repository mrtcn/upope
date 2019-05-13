using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Upope.Identity.ViewModels;
using Upope.Loyalty.EntityParams;
using Upope.Loyalty.Services.Interfaces;
using Upope.Loyalty.ViewModels;
using Upope.ServiceBase.Extensions;
using Upope.ServiceBase.Handler;

namespace Upope.Loyalty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyController : ControllerBase
    {
        private readonly ILoyaltyService _loyaltyService;
        private readonly IIdentityService _identityService;
        private readonly IHttpHandler _httpHandler;
        private readonly IMapper _mapper;

        public LoyaltyController(
            ILoyaltyService loyaltyService,
            IIdentityService identityService,
            IMapper mapper, 
            IHttpHandler httpHandler)
        {
            _loyaltyService = loyaltyService;
            _identityService = identityService;
            _httpHandler = httpHandler;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [Route("GetPoint")]
        public IActionResult GetPoint()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            //TODO: GetLoyaltyByUserId does not accept accesstoken. Should be fixed
            var point = _loyaltyService.GetLoyaltyByUserId(accessToken);

            var pointViewModel = _mapper.Map<GetPointViewModel>(point);

            return Ok(pointViewModel);
        }

        [HttpPost]
        [Authorize]
        [Route("SufficientPoints")]
        public async Task<IActionResult> SufficientPoints(GetSufficientPointViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var sufficientPoints = _loyaltyService.SufficientPoints(userId, model.Points);
            var userIds = sufficientPoints.Select(x => x.UserId).ToList();

            return Ok(userIds);
        }

        [HttpPost]
        [Authorize]
        [Route("ChargeCredits")]
        public IActionResult ChargeCredits(ChargeCreditsViewModel model)
        {
            var chargeGameCreditsParams = _mapper.Map<ChargeGameCreditsParams>(model);
            _loyaltyService.ChargeGameCredits(chargeGameCreditsParams);

            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate(CreateOrUpdateViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var loyaltyParams = _mapper.Map<CreateOrUpdateViewModel, LoyaltyParams>(model);

            var loyalty = _loyaltyService.GetLoyaltyByUserId(userId);

            if (loyalty != null)
                loyaltyParams.Id = loyalty.Id;

            _loyaltyService.CreateOrUpdate(loyaltyParams);

            var result = _mapper.Map<LoyaltyParams, CreateOrUpdateViewModel>(loyaltyParams);

            return Ok(result);
        }
    }
}