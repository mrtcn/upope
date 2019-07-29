using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Upope.Game.EntityParams;
using Upope.Game.Services.Interfaces;
using Upope.Game.ViewModels;
using Upope.ServiceBase.Extensions;

namespace Upope.Game.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IGameRoundService _gameRoundService;
        private readonly IBluffService _bluffService;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public GameController(
            IGameService gameService,
            IGameRoundService gameRoundService,
            IBluffService bluffService,
            IIdentityService identityService,
            IMapper mapper)
        {
            _gameService = gameService;
            _gameRoundService = gameRoundService;
            _bluffService = bluffService;
            _identityService = identityService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [Route("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate(CreateOrUpdateViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var gameParams = _mapper.Map<CreateOrUpdateViewModel, GameParams>(model);
            _gameService.CreateOrUpdate(gameParams);

            var gameRoundParams = new GameRoundParams(gameParams.Id);
            gameRoundParams.Round = gameRoundParams.Id == 0 ? 1 : gameRoundParams.Round;

            _gameRoundService.CreateOrUpdate(gameRoundParams);

            _gameService.SendGameCreatedMessage(new Services.Models.GameCreatedModel(gameParams.Id, gameParams.HostUserId, gameParams.GuestUserId));
           
            var result = _mapper.Map<GameParams, CreateOrUpdateViewModel>(gameParams);

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("SendChoice")]
        public async Task<IActionResult> SendChoice(SendChoiceViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var sendChoiceParams = _mapper.Map<SendChoiceViewModel, SendChoiceParams>(model);
            sendChoiceParams.UserId = userId;

            var gameRoundParams = await _gameRoundService.SendChoice(sendChoiceParams);
            await _bluffService.AskBluff(userId, gameRoundParams);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("SendBluff")]
        public async Task<IActionResult> SendBluff(SendBluffViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var lastGameRound = _gameRoundService.GetLastRoundEntity(model.GameId);
            if (lastGameRound.GuestAnswer != Enum.RockPaperScissorsType.NotAnswered && lastGameRound.HostAnswer != Enum.RockPaperScissorsType.NotAnswered)
            {
                return BadRequest("Rakip seçimini yaptı, blöf için çok geç");
            }

            BluffParams bluffParams = _bluffService.GetBluffParams(model, userId, lastGameRound);
            _bluffService.CreateOrUpdate(bluffParams);
            await _bluffService.TextBluff(model, userId);

            return Ok();
        }
    }
}