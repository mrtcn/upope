using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Upope.Game.Hubs;
using Upope.Game.Models;
using Upope.Game.Services.Interfaces;
using Upope.Game.Services.Models;
using Upope.ServiceBase;
using Upope.ServiceBase.Models;
using Upope.ServiceBase.Services.Interfaces;
using GameEntity = Upope.Game.Data.Entities.Game;

namespace Upope.Game.Services
{    
    public class GameService : EntityServiceBase<GameEntity>, IGameService
    {
        private readonly IHubContext<GameHub> _hubContext;
        private readonly IUserService _userService;
        private readonly IGeoLocationService _geoLocationService;

        public GameService(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper,
            IHubContext<GameHub> hubContext,
            IUserService userService,
            IGeoLocationService geoLocationService) : base(applicationDbContext, mapper)
        {
            _hubContext = hubContext;
            _userService = userService;
            _geoLocationService = geoLocationService;
        }

        protected override void OnSaveChangedAsync(IEntityParams entityParams, GameEntity entity)
        {
            base.OnSaveChangedAsync(entityParams, entity);
        }

        public RematchUserInfo RematchUserInfo(string userId, string requestingUserId, int credit, int maxCredit)
        {
            var user = _userService.GetUserByUserId(userId);
            var requestingUser = _userService.GetUserByUserId(requestingUserId);
            return new RematchUserInfo {
                Username = requestingUser.Nickname,
                ImagePath = requestingUser.PictureUrl,
                Distance = _geoLocationService.GetDistance(new CoordinateModel(user.Latitude, user.Longitude), new CoordinateModel(requestingUser.Latitude, requestingUser.Longitude)),
                Credit = credit,
                MaxCredit = maxCredit
            };
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

        public int StreakCount(string userId)
        {
            var maxLostId = Entities
                .Where(x => (x.GuestUserId == userId || x.HostUserId == userId) && x.WinnerId != userId && x.Status == ServiceBase.Enums.Status.Active)
                .Max(x => x.Id);

            var streakCount = Entities.Where(x => x.Id > maxLostId && x.WinnerId == userId && x.Status == ServiceBase.Enums.Status.Active).Count();

            return streakCount;
        }

        public int LatestGameId(string userId)
        {
            var latestGameId = Entities
                .Where(x => (x.GuestUserId == userId || x.HostUserId == userId) && x.Status == ServiceBase.Enums.Status.Active)
                .Max(x => x.Id);

            return latestGameId;
        }

        public int LatestWinGameId(string userId)
        {
            var latestWinGameId = Entities
                .Where(x => (x.GuestUserId == userId || x.HostUserId == userId) && x.WinnerId == userId && x.Status == ServiceBase.Enums.Status.Active)
                .Max(x => x.Id);

            return latestWinGameId;
        }
    }
}
