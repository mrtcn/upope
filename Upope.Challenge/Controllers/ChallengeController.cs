using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Upope.Challenge.CustomExceptions;
using Upope.Challenge.EntityParams;
using Upope.Challenge.Services.Interfaces;
using Upope.Challenge.Services.Models;
using Upope.Challenge.ViewModels;
using Upope.ServiceBase.Extensions;

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

        public ChallengeController(
            IChallengeService challengeService,
            IIdentityService identityService,
            IMapper mapper,
            IChallengeRequestService challengeRequestService)
        {
            _challengeService = challengeService;
            _identityService = identityService;
            _mapper = mapper;
            _challengeRequestService = challengeRequestService;
        }

        [HttpPost]
        [Authorize]
        [Route("ChallengeRequests")]
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
            var challengeParams = new ChallengeParams(ServiceBase.Enums.Status.Active, userId, random.Next(1, 100));
            var challenge = _challengeService.CreateOrUpdate(challengeParams);

            var challengerIds = await _challengeRequestService
                .CreateChallengeRequests(
                new CreateChallengeRequestModel(
                    accessToken,
                    challenge.Id,
                    userId,
                    challenge.RewardPoint));

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
            var userId = await _identityService.GetUserId(accessToken);

            var challengeParams = new ChallengeParams(ServiceBase.Enums.Status.Active, userId, model.RewardPoint);
            var challenge = _challengeService.CreateOrUpdate(challengeParams);

            var challengerIds = await _challengeRequestService
                .CreateChallengeRequests(
                new CreateChallengeRequestModel(
                    accessToken, 
                    challenge.Id, 
                    userId, 
                    challenge.RewardPoint));

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
        [Route("UpdateChallenge")]
        public async Task<IActionResult> UpdateChallenge(UpdateChallengeInputViewModel model)
        {
            try
            {
                var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
                var userId = await _identityService.GetUserId(accessToken);

                var challengeRequest = _challengeRequestService.Get(model.ChallengeRequestId);

                if(challengeRequest.ChallengerId != userId)
                    return BadRequest(challengeRequest.ChallengerId + " - " + userId + " : " + "An error occured. Please select another game or try in a few seconds again.");

                var challengeRequestParams = _mapper.Map<ChallengeRequestParams>(challengeRequest);
                challengeRequestParams.ChallengeRequestStatus = model.ChallengeRequestStatus;

                _challengeRequestService.CreateOrUpdate(challengeRequestParams);

                if(challengeRequestParams.ChallengeRequestStatus == Enums.ChallengeRequestStatus.Accepted)
                {

                }
            }
            catch(UserNotAvailableException ex)
            {
                return BadRequest("User is in another game. Please select another game");
            }
            catch (Exception ex)
            {
                return BadRequest(ex + "An error occured. Please select another game or try in a few seconds again.");
            }

            return Ok();
        }


        [HttpPost]
        [Authorize]
        [Route("ChallengeAccepted")]
        public async Task<IActionResult> ChallengeAccepted(ChallengeAcceptedViewModel model)
        {
            // Challenge is rejected by 3 people and then accepted by someone

            return Ok();
        }
    }
}