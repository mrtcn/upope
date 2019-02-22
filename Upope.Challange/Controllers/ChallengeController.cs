using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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

            var challengerRequestParamList = _challengeRequestService.ChallengeRequests(userId);

            return Ok(challengerRequestParamList);
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
            var challengeRequest = _challengeRequestService.Get(model.ChallengeRequestId);
            var challengeRequestParams = _mapper.Map<ChallengeRequestParams>(challengeRequest);
            challengeRequestParams.ChallengeRequestStatus = model.ChallengeRequestStatus;

            _challengeRequestService.CreateOrUpdate(challengeRequestParams);

            if(model.ChallengeRequestStatus == Enums.ChallengeRequestStatus.Accepted)
            {
                _challengeRequestService.SetChallengeRequestsToMissed(challengeRequestParams.ChallengeId, challengeRequestParams.Id);
            }
            return Ok();
        }
    }
}