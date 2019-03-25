using AutoMapper;
using Upope.Game.Data.Entities;
using Upope.Game.Services.Interfaces;
using Upope.ServiceBase;

namespace Upope.Game.Services
{    
    public class GameRoundService : EntityServiceBase<GameRound>, IGameRoundService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GameRoundService(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }
    }
}
