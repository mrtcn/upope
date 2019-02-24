using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Upope.Challange.CustomExceptions;
using Upope.Challange.EntityParams;
using Upope.Challange.Hubs;
using Upope.Challange.Services.Interfaces;
using Upope.Challange.Services.Models;
using Upope.Challange.ViewModels;
using Upope.ServiceBase.Extensions;

namespace Upope.Challange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeService _challengeService;
        private readonly IMapper _mapper;
        private readonly IChallengeRequestService _challengeRequestService;
        private readonly IHubContext<ChallengeHubs> _hubContext;

        public ChallengeController(
            IChallengeService challengeService,
            IMapper mapper,
            IHubContext<ChallengeHubs> hubContext,
            IChallengeRequestService challengeRequestService)
        {
            _challengeService = challengeService;
            _hubContext = hubContext;
            _mapper = mapper;
            _challengeRequestService = challengeRequestService;
        }

        [HttpPost]
        [Route("ChallengeRequests")]
        public async Task<IActionResult> ChallengeRequests()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _challengeService.GetUserId(accessToken);

            var challengerRequestList = _challengeRequestService.ChallengeRequests(userId);

            return Ok(challengerRequestList);
        }

        [HttpPost]
        [Authorize]
        [Route("CreateChallenge")]
        public async Task<IActionResult> CreateChallenge(CreateChallengeViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _challengeService.GetUserId(accessToken);

            var challengeParams = new ChallengeParams(ServiceBase.Enums.Status.Active, userId, model.RewardPoint);
            var challenge = _challengeService.CreateOrUpdate(challengeParams);

            var challengerIds = await _challengeRequestService.CreateChallengeRequests(new CreateChallengeRequestModel(accessToken, challenge.Id, userId, challenge.RewardPoint));

            return Ok(challengeParams);
        }

        [HttpPost]
        [Authorize]
        [Route("UpdateChallenge")]
        public IActionResult UpdateChallenge(UpdateChallengeInputViewModel model)
        {
            try
            {
                var challengeRequest = _challengeRequestService.Get(model.ChallengeRequestId);
                var challengeRequestParams = _mapper.Map<ChallengeRequestParams>(challengeRequest);
                challengeRequestParams.ChallengeRequestStatus = model.ChallengeRequestStatus;

                _challengeRequestService.CreateOrUpdate(challengeRequestParams);
            }
            catch(UserNotAvailableException ex)
            {
                return BadRequest("User is in another game. Please select another game");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured during creating a new game. Please select another game or try in a few seconds again.");
            }

            return Ok();
        }
    }
}