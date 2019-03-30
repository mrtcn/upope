using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Upope.Challenge.Hubs;
using Upope.Game.Data.Entities;
using Upope.Game.Services.Interfaces;
using Upope.ServiceBase;

namespace Upope.Game.Services
{    
    public class GameRoundService : EntityServiceBase<GameRound>, IGameRoundService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IHubContext<GameHubs> _hubContext;

        public GameRoundService(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper,
            IHubContext<GameHubs> hubContext) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _hubContext = hubContext;
        }
    }
}
