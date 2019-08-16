using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Upope.Identity.ViewModels;
using Upope.Loyalty.EntityParams;
using Upope.Loyalty.Interfaces;
using Upope.Loyalty.Services.Interfaces;
using Upope.Loyalty.ViewModels;
using Upope.ServiceBase.Extensions;

namespace Upope.Loyalty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyController : ControllerBase
    {
        private readonly ILoyaltyService _loyaltyService;
        private readonly INotificationManager _notificationManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<LoyaltyController> _localizer;

        public LoyaltyController(
            ILoyaltyService loyaltyService,
            INotificationManager notificationManager,
            IStringLocalizer<LoyaltyController> localizer,
            IMapper mapper)
        {
            _loyaltyService = loyaltyService;
            _notificationManager = notificationManager;
            _mapper = mapper;
            _localizer = localizer;
        }

        [HttpGet]
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

        [HttpGet]
        [Authorize]
        [Route("SufficientPoints/{points}")]
        public async Task<IActionResult> SufficientPoints(int points)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();

            var sufficientPoints = await _loyaltyService.SufficientPoints(accessToken, points);
            var userIds = sufficientPoints.Select(x => x.UserId).ToList();

            return Ok(userIds);
        }

        [HttpPut]
        [Authorize]
        [Route("ChargeCredits")]
        public async Task<IActionResult> ChargeCredits(CreditsViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();

            var chargeCreditsParams = _mapper.Map<ChargeCreditsParams>(model);
            _loyaltyService.ChargeCredits(chargeCreditsParams);
            await _notificationManager.SendNotification(accessToken, model.UserId);

            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("AddCredits")]
        public async Task<IActionResult> AddCredits(CreditsViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();

            var chargeCreditsParams = _mapper.Map<ChargeCreditsParams>(model);
            _loyaltyService.AddCredits(chargeCreditsParams);
            await _notificationManager.SendNotification(accessToken, model.UserId);

            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate(CreateOrUpdateViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();

            var loyaltyParams = _mapper.Map<CreateOrUpdateViewModel, LoyaltyParams>(model);

            var loyalty = _loyaltyService.GetLoyaltyByUserId(accessToken);

            if (loyalty != null)
                loyaltyParams.Id = loyalty.Id;

            _loyaltyService.CreateOrUpdate(loyaltyParams);

            var result = _mapper.Map<LoyaltyParams, CreateOrUpdateViewModel>(loyaltyParams);

            return Ok(result);
        }
    }
}