﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Upope.Identity.ViewModels;
using Upope.Loyalty.EntityParams;
using Upope.Loyalty.Interfaces;
using Upope.Loyalty.Services.Interfaces;
using Upope.Loyalty.ViewModels;
using Upope.ServiceBase.Extensions;
using Upope.ServiceBase.Helpers;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.Loyalty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyController : CustomControllerBase
    {
        private readonly ILoyaltyService _loyaltyService;
        private readonly IIdentityService _identityService;
        private readonly INotificationManager _notificationManager;
        private readonly IMapper _mapper;

        public LoyaltyController(
            ILoyaltyService loyaltyService,
            IIdentityService identityService,
            INotificationManager notificationManager,
            IMapper mapper)
        {
            _loyaltyService = loyaltyService;
            _identityService = identityService;
            _notificationManager = notificationManager;
            _mapper = mapper;
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

        [HttpPost]
        [Authorize]
        [Route("FilterUsers")]
        public async Task<IActionResult> FilterUsers(FilterUsersViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();

            var sufficientPoints = await _loyaltyService.SufficientPoints(accessToken, model.Point, model.IsBotActivated);
            var sufficientUserIds = sufficientPoints.Select(x => x.UserId).ToList();
            var userIds = _loyaltyService.ExcludeOutOfRangeUsers(model.Range, model.ChallengeOwnerId, sufficientUserIds);
            

            return Ok(userIds);
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

            return Ok(true);
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

            return Ok(true);
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

            return Ok(true);
        }

        [HttpPut("AddWin/{userId}")]
        [Authorize]
        public IActionResult AddWin(string userId)
        {
            _loyaltyService.AddWin(userId);

            //TODO: (Murat) AddScores notifications should be clarified!
            //await _notificationManager.SendNotification(accessToken, model.UserId);

            return Ok(true);
        }

        [HttpPut("ResetWin/{userId}")]
        [Authorize]
        public IActionResult ResetWin(string userId)
        {
            _loyaltyService.ResetWin(userId);

            //TODO: (Murat) AddScores notifications should be clarified!
            //await _notificationManager.SendNotification(accessToken, model.UserId);

            return Ok(true);
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