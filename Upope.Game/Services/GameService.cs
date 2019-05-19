using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Upope.Game.Hubs;
using Upope.Game.Services.Interfaces;
using Upope.Game.Services.Models;
using Upope.ServiceBase;
using Upope.ServiceBase.Models;
using GameEntity = Upope.Game.Data.Entities.Game;

namespace Upope.Game.Services
{    
    public class GameService : EntityServiceBase<GameEntity>, IGameService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IHubContext<GameHubs> _hubContext;

        public GameService(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper,
            IHubContext<GameHubs> hubContext) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        protected override void OnSaveChangedAsync(IEntityParams entityParams, GameEntity entity)
        {
            base.OnSaveChangedAsync(entityParams, entity);
        }

        public bool IsHostUser(int gameId, string userId)
        {
            return Entities.FirstOrDefault(x => x.Id == gameId && x.HostUserId == userId) != null;
        }

        public void SendGameCreatedMessage(GameCreatedModel model)
        {
            _hubContext.Clients.Users(new List<string>() { model.HostUserId, model.GuestUserId })
            .SendAsync("GameCreated", JsonConvert.SerializeObject(model));
        }
    }
}
