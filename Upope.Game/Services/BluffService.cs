using AutoMapper;
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
        private readonly IMapper _mapper;

        public BluffService(
            ApplicationDbContext applicationDbContext,
            IGameService gameService,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
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
    }
}
