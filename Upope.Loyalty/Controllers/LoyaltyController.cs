using System.Collections.Generic;
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
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.Loyalty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyController : ControllerBase
    {
        private readonly ILoyaltyService _loyaltyService;
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;
        private readonly INotificationManager _notificationManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<LoyaltyController> _localizer;

        public LoyaltyController(
            ILoyaltyService loyaltyService,
            IUserService userService,
            IIdentityService identityService,
            INotificationManager notificationManager,
            IStringLocalizer<LoyaltyController> localizer,
            IMapper mapper)
        {
            _loyaltyService = loyaltyService;
            _userService = userService;
            _identityService = identityService;
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

        [HttpPut]
        [Authorize]
        [Route("AddScores")]
        public IActionResult AddScores(PointViewModel model)
        {
            var addCreditsParams = _mapper.Map<AddScoresParams>(model);
            _loyaltyService.AddScores(addCreditsParams);

            //TODO: (Murat) AddScores notifications should be clarified!
            //await _notificationManager.SendNotification(accessToken, model.UserId);

            return Ok();
        }

        [HttpPut("AddWin/{userId}")]
        [Authorize]
        public IActionResult AddWin(string userId)
        {
            _loyaltyService.AddWin(userId);

            //TODO: (Murat) AddScores notifications should be clarified!
            //await _notificationManager.SendNotification(accessToken, model.UserId);

            return Ok();
        }

        [HttpPut("ResetWin/{userId}")]
        [Authorize]
        public IActionResult ResetWin(string userId)
        {
            _loyaltyService.ResetWin(userId);

            //TODO: (Murat) AddScores notifications should be clarified!
            //await _notificationManager.SendNotification(accessToken, model.UserId);

            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("CreateOrUpdate")]
        public IActionResult CreateOrUpdate(CreateOrUpdateViewModel model)
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

        [HttpGet("UserStats/{userId}")]
        [Authorize]
        public IActionResult GetUserStats(string userId)
        {
            var userStats = _loyaltyService.GetUserStats(userId);
            return Ok(userStats);
        }

        [HttpGet("WinLeadershipBoard/{page}")]
        [Authorize]
        public async Task<IActionResult> WinLeadershipBoard(int page)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var winLeadershipBoard = _loyaltyService.WinLeaderships(page);
            var winLeadershipBoardViewModel = _mapper.Map<List<WinLeadershipBoardViewModel>>(winLeadershipBoard);

            var userStats = _loyaltyService.GetUserStats(userId);
            var userStatsViewModel = _mapper.Map<UserStatsViewModel>(userStats);

            return Ok(new { LeadershipBoard = winLeadershipBoardViewModel, UserStats = userStatsViewModel});
        }

        [HttpGet("ScoreLeadershipBoard/{page}")]
        [Authorize]
        public async Task<IActionResult> ScoreLeadershipBoard(int page)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var scoreLeadershipBoard = _loyaltyService.ScoreLeaderships(page);
            var scoreLeadershipBoardViewModel = _mapper.Map<List<WinLeadershipBoardViewModel>>(scoreLeadershipBoard);

            var userStats = _loyaltyService.GetUserStats(userId);
            var userStatsViewModel = _mapper.Map<UserStatsViewModel>(userStats);

            return Ok(new { LeadershipBoard = scoreLeadershipBoardViewModel, UserStats = userStatsViewModel });
        }

        [HttpGet("CreditLeadershipBoard/{page}")]
        [Authorize]
        public async Task<IActionResult> CreditLeadershipBoard(int page)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var creditLeadershipBoard = _loyaltyService.CreditLeaderships(page);
            var creditLeadershipBoardViewModel = _mapper.Map<List<CreditLeadershipBoardViewModel>>(creditLeadershipBoard);

            var userStats = _loyaltyService.GetUserStats(userId);
            var userStatsViewModel = _mapper.Map<UserStatsViewModel>(userStats);

            return Ok(new { LeadershipBoard = creditLeadershipBoardViewModel, UserStats = userStatsViewModel });
        }
    }
}