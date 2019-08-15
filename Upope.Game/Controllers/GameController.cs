using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Upope.Game.CustomException;
using Upope.Game.Interfaces;
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
        private readonly IStringLocalizer<GameController> _localizer;
        private readonly IContactService _contactService;

        public GameController(
            IIdentityService identityService,
            IGameManager gameManager,
            IStringLocalizer<GameController> localizer,
            IContactService contactService)
        {
            _identityService = identityService;
            _gameManager = gameManager;
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
    }
}