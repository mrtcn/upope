using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Upope.Game.CustomException;
using Upope.Game.Interfaces;
using Upope.Game.Models;
using Upope.Game.Services.Interfaces;
using Upope.Game.ViewModels;
using Upope.ServiceBase.Extensions;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.Game.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IGameManager _gameManager;
        private readonly ILoyaltyService _loyaltyService;
        private readonly IStringLocalizer<GameController> _localizer;
        private readonly IContactService _contactService;

        public GameController(
            IIdentityService identityService,
            IGameManager gameManager,
            ILoyaltyService loyaltyService,
            IStringLocalizer<GameController> localizer,
            IContactService contactService)
        {
            _identityService = identityService;
            _gameManager = gameManager;
            _loyaltyService = loyaltyService;
            _localizer = localizer;
            _contactService = contactService;
        }

        [HttpPost]
        [Authorize]
        [Route("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate(CreateOrUpdateViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();

            var gameRoundParams = _gameManager.CreateOrUpdateGame(model);

            //If the game has just began, a contact record should be created which indicates they are in contact and can start chat
            if (gameRoundParams.Round == 1)
                await _contactService.SyncContactTable(accessToken, model.HostUserId, model.GuestUserId);

            return Ok(new CreateOrUpdateViewModel(
                gameRoundParams.GameId, 
                gameRoundParams.Id, 
                gameRoundParams.Round, 
                model.HostUserId, 
                model.GuestUserId, 
                model.Credit));
        }

        [HttpPost]
        [Authorize]
        [Route("SendChoice")]
        public async Task<IActionResult> SendChoice(SendChoiceViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            try
            {
                var roundEndModel = await _gameManager.SendChoice(model, userId, accessToken);
                return Ok(roundEndModel);
            }
            catch (AlreadyAnsweredException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("SendBluff")]
        public async Task<IActionResult> SendBluff(SendBluffViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var isSuccess = await _gameManager.SendBluff(userId, model);
            if (isSuccess)
            {
                return BadRequest(_localizer.GetString("ExpiredBluff").Value);
            }

            return Ok();
        }

        [HttpGet("Rematch/{requestedUserId}")]
        [Authorize]
        public async Task<IActionResult> Rematch(string requestedUserId)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var requestingUserStats = await _loyaltyService.GetLoyalty(accessToken, userId);
            if (requestingUserStats.Credit == 0)
            {
                return BadRequest(_localizer.GetString("NotEnoughCreditForRematch").Value);
            }

            var requestedUserStats = await _loyaltyService.GetLoyalty(accessToken, requestedUserId);
            if (requestedUserStats.Credit == 0)
            {
                return BadRequest(_localizer.GetString("RequestedUserNotEnoughCredit").Value);
            }

            return Ok(new { MaxCredit = Math.Max(requestedUserStats.Credit, requestingUserStats.Credit)});
        }

        [HttpPost("SendRematchRequest")]
        [Authorize]
        public async Task<IActionResult> SendRematchRequest(RematchRequestModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            _gameManager.SendRematch(model.UserId, userId, model.Credit, model.MaxCredit);

            return Ok();
        }

        [HttpPost("RaiseRematchRequest")]
        [Authorize]
        public async Task<IActionResult> RaiseRematch(RematchRequestModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            _gameManager.SendRematchRaise(model.UserId, userId, model.Credit, model.MaxCredit);

            return Ok();
        }

        [HttpPost("AcceptRematch")]
        [Authorize]
        public async Task<IActionResult> AcceptRematch(RematchRequestModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            _gameManager.CreateOrUpdateGame(new CreateOrUpdateViewModel(0, 0, 1, userId, model.UserId, model.Credit, true));

            return Ok();
        }

        [HttpPost("RejectRematch")]
        [Authorize]
        public async Task<IActionResult> RejectRematch(RejectRematchModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            _gameManager.RejectRematch(model.UserId, userId);

            return Ok();
        }
    }
}