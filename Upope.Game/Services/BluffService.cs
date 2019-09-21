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
        private readonly IGameService _gameService;
        private readonly IRoundAnswerService _roundAnswerService;
        private readonly IHubContext<GameHub> _hubContext;
        private readonly IMapper _mapper;

        public BluffService(
            ApplicationDbContext applicationDbContext,
            IGameService gameService,
            IRoundAnswerService roundAnswerService,
            IHubContext<GameHub> hubContext,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _mapper = mapper;
            _hubContext = hubContext;
            _gameService = gameService;
            _roundAnswerService = roundAnswerService;
        }

        public BluffParams GetBluffParams(SendBluffViewModel model, string userId, RockPaperScissorsType choice)
        {
            var bluffParams = _mapper.Map<SendBluffViewModel, BluffParams>(model);
            bluffParams.UserId = userId;
            bluffParams.GameRoundId = model.GameRoundId; 

            return bluffParams;
        }

        public bool IsFirstAnswer(int gameRoundId)
        {
            var answerCount = _roundAnswerService.RoundAnswers(gameRoundId).Count;

            return answerCount < 2 ? true : false;
        }

        public async Task AskBluff(string userId, int gameId, int gameRoundId)
        {
            if (IsFirstAnswer(gameRoundId))
            {
                var isHost = _gameService.IsHostUser(gameId, userId);
                var game = _gameService.Get(gameId);
                var askBluffUserId = isHost ? game.GuestUserId : game.HostUserId;

                await _hubContext.Clients.User(askBluffUserId).SendAsync("AskBluff", "AskBluff");
            }
        }

        public async Task TextBluff(SendBluffViewModel model, string userId)
        {
            var isHostUser = _gameService.IsHostUser(model.GameId, userId);

            await _hubContext.Clients.User(userId).SendAsync("TextBluff", JsonConvert.SerializeObject(new { choice = model.Choice }));
        }
    }
}
