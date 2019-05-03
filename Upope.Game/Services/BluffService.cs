using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Upope.Challenge.Hubs;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.Game.Services.Interfaces;
using Upope.Game.ViewModels;
using Upope.ServiceBase;

namespace Upope.Game.Services
{    
    public class BluffService : EntityServiceBase<Bluff>, IBluffService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IGameService _gameService;
        private readonly IGameRoundService _gameRoundService;
        private readonly IMapper _mapper;
        private readonly IHubContext<GameHubs> _hubContext;

        public BluffService(
            ApplicationDbContext applicationDbContext,
            IGameService gameService,
            IGameRoundService gameRoundService,
            IMapper mapper,
            IHubContext<GameHubs> hubContext) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _hubContext = hubContext;
            _gameService = gameService;
            _gameRoundService = gameRoundService;
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
    }
}
