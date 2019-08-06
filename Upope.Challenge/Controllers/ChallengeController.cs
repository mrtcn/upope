using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Upope.Challenge.CustomExceptions;
using Upope.Challenge.EntityParams;
using Upope.Challenge.Services.Interfaces;
using Upope.Challenge.Services.Models;
using Upope.Challenge.ViewModels;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Extensions;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeService _challengeService;
        private readonly IMapper _mapper;
        private readonly IChallengeRequestService _challengeRequestService;
        private readonly IIdentityService _identityService;
        private readonly IStringLocalizer<ChallengeController> _localizer;

        public ChallengeController(
            IChallengeService challengeService,
            IIdentityService identityService,
            IMapper mapper,
            IChallengeRequestService challengeRequestService,
            IStringLocalizer<ChallengeController> localizer)
        {
            _challengeService = challengeService;
            _identityService = identityService;
            _mapper = mapper;
            _challengeRequestService = challengeRequestService;
            _localizer = localizer;
        }

        [HttpGet("ChallengeRequests")]
        [Authorize]
        public async Task<IActionResult> ChallengeRequests()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var challengerRequestList = _challengeRequestService.ChallengeRequests(userId);

            return Ok(challengerRequestList);
        }

        [HttpPost]
        [Authorize]
        [Route("TriggerChallenges")]
        public async Task<IActionResult> TriggerChallenges()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var random = new Random();
            var challengeParams = new ChallengeParams(Status.Active, userId, random.Next(1, 100));
            var challenge = _challengeService.CreateOrUpdate(challengeParams);

            var challengerIds = await _challengeRequestService
                .CreateChallengeRequests(
                new CreateChallengeRequestModel(
                    accessToken,
                    challenge.Id,
                    userId,
                    challenge.RewardPoint,
                    20,
                    Gender.Unknown));

            await _challengeRequestService
                .RejectAcceptChallenge(
                new RejectAcceptChallengeModel()
                {
                    AccessToken = accessToken,
                    ChallengeId = challenge.Id,
                    UserId = userId
                });

            return Ok(challengeParams);
        }

        [HttpPost]
        [Authorize]
        [Route("CreateChallenge")]
        public async Task<IActionResult> CreateChallenge(CreateChallengeViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userProfile = await _identityService.GetUserProfile(accessToken);

            if (userProfile.UserType == UserType.Basic && model.Range != 0 && model.Sex != Gender.Unknown)
                return Unauthorized(_localizer.GetString("NotPremium").Value);

            var challengeParams = new ChallengeParams(Status.Active, userProfile.Id, model.BetAmount);
            var challenge = _challengeService.CreateOrUpdate(challengeParams);

            var challengerIds = await _challengeRequestService
                .CreateChallengeRequests(
                new CreateChallengeRequestModel(
                    accessToken, 
                    challenge.Id,
                    userProfile.Id, 
                    challenge.RewardPoint,
                    model.Range,
                    model.Sex));
            if (challengerIds == null || challengerIds.Count == 0)
            {
                _challengeService.Remove(new RemoveEntityParams(challenge.Id, new HasOperator(), true, true));
                return BadRequest(_localizer.GetString("NoUserFound").Value);
            }
                

            return Ok(challengeParams);
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateChallenge/{challengeRequestId}")]
        public async Task<IActionResult> UpdateChallenge(int challengeRequestId, [FromBody]UpdateChallengeInputViewModel model)
        {
            try
            {
                var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
                var userId = await _identityService.GetUserId(accessToken);

                var challengeRequest = _challengeRequestService.Get(challengeRequestId);

                if(challengeRequest.ChallengerId != userId || challengeRequest.ChallengeRequestStatus != Enums.ChallengeRequestStatus.Waiting)
                    return BadRequest(_localizer.GetString("GameAlreadyStarted").Value);

                var challengeRequestParams = _mapper.Map<ChallengeRequestParams>(challengeRequest);
                challengeRequestParams.ChallengeRequestStatus = model.ChallengeRequestAnswer;
                challengeRequestParams.AccessToken = accessToken;

                _challengeRequestService.CreateOrUpdate(challengeRequestParams);
            }
            catch(UserNotAvailableException ex)
            {
                return BadRequest(_localizer.GetString("GameAlreadyStarted").Value);
            }
            catch (Exception ex)
            {
                return BadRequest(_localizer.GetString("UpdateChallengeException").Value);
            }

            return Ok();
        }
    }
}