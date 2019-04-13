using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public GameController(
            IGameService gameService,
            IGameRoundService gameRoundService,
            IIdentityService identityService,
            IMapper mapper)
        {
            _gameService = gameService;
            _gameRoundService = gameRoundService;
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

            _gameRoundService.SendChoice(sendChoiceParams);

            return Ok();
        }
    }
}