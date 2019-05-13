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
using Upope.Game.Services.Interfaces;
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
        private readonly IGameSyncService _gameSyncService;
        private readonly IIdentityService _identityService;

        public ChallengeController(
            IChallengeService challengeService,
            IGameSyncService gameSyncService,
            IIdentityService identityService,
            IMapper mapper,
            IChallengeRequestService challengeRequestService)
        {
            _challengeService = challengeService;
            _gameSyncService = gameSyncService;
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
                    challenge.RewardPoint,
                    20));

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

            var challengeParams = new ChallengeParams(ServiceBase.Enums.Status.Active, userId, model.BetAmount);
            var challenge = _challengeService.CreateOrUpdate(challengeParams);

            var challengerIds = await _challengeRequestService
                .CreateChallengeRequests(
                new CreateChallengeRequestModel(
                    accessToken, 
                    challenge.Id, 
                    userId, 
                    challenge.RewardPoint,
                    model.Range));
            if (challengerIds == null || challengerIds.Count == 0)
            {
                _challengeService.Remove(new RemoveEntityParams(challenge.Id, new HasOperator(), true, true));
                return BadRequest("Belirtilen kriterlerde bir kullanıcı bulunamadı");
            }
                

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
                challengeRequestParams.ChallengeRequestStatus = model.ChallengeRequestAnswer;

                _challengeRequestService.CreateOrUpdate(challengeRequestParams);

                if(challengeRequestParams.ChallengeRequestStatus == Enums.ChallengeRequestStatus.Accepted)
                {
                    challengeRequest.Challenge = _challengeService.Get(challengeRequest.ChallengeId);

                    var createOrUpdateGameViewModel = new CreateOrUpdateGameViewModel() {
                        Id = 0,
                        Credit = challengeRequest.Challenge.RewardPoint,
                        GuestUserId = challengeRequest.ChallengerId,
                        HostUserId = challengeRequest.ChallengeOwnerId
                    };

                    await _gameSyncService.SyncGameTable(createOrUpdateGameViewModel, accessToken);
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
    }
}