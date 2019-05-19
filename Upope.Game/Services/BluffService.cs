using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.Game.Enum;
using Upope.Game.Hubs;
using Upope.Game.Services.Interfaces;
using Upope.Game.ViewModels;
using Upope.ServiceBase;

namespace Upope.Game.Services
{    
    public class BluffService : EntityServiceBase<Bluff>, IBluffService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IGameService _gameService;
        private readonly IHubContext<GameHubs> _hubContext;
        private readonly IMapper _mapper;

        public BluffService(
            ApplicationDbContext applicationDbContext,
            IGameService gameService,
            IHubContext<GameHubs> hubContext,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _hubContext = hubContext;
            _gameService = gameService;
        }

        public BluffParams GetBluffParams(SendBluffViewModel model, string userId, GameRoundParams lastGameRound)
        {
            var isHost = _gameService.IsHostUser(model.GameId, userId);
            var userChoice = isHost ? lastGameRound.HostAnswer : lastGameRound.GuestAnswer;

            var bluffParams = _mapper.Map<SendBluffViewModel, BluffParams>(model);
            bluffParams.UserId = userId;
            bluffParams.GameRoundId = lastGameRound.Id;
            bluffParams.IsSuperBluff = bluffParams.Choice == userChoice;
            return bluffParams;
        }

        public bool IsFirstAnswer(GameRoundParams gameRoundParams)
        {
            if (gameRoundParams.GuestAnswer == RockPaperScissorsType.NotAnswered || gameRoundParams.HostAnswer == RockPaperScissorsType.NotAnswered)
            {
                return true;
            }

            return false;
        }

        public async Task AskBluff(string userId, GameRoundParams gameRoundParams)
        {
            if (IsFirstAnswer(gameRoundParams))
            {
                var isHost = _gameService.IsHostUser(gameRoundParams.GameId, userId);
                var game = _gameService.Get(gameRoundParams.GameId);
                var askBluffUserId = isHost ? game.GuestUserId : game.HostUserId;
                await _hubContext.Clients.User(askBluffUserId).SendAsync("AskBluff", "AskBluff");
            }
        }

        public async Task TextBluff(SendBluffViewModel model, string userId)
        {
            var isHostUser = _gameService.IsHostUser(model.GameId, userId);

            var game = _gameService.Get(model.GameId);
            await _hubContext.Clients.User(userId).SendAsync("TextBluff", JsonConvert.SerializeObject(new { choice = model.Choice }));
        }
    }
}
