using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upope.Game.Hubs;
using Upope.Game.Models;
using Upope.Game.Services.Interfaces;
using Upope.Game.Services.Models;
using Upope.Game.ViewModels;
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
        private readonly IBotService _botService;

        public GameService(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper,
            IHubContext<GameHub> hubContext,
            IUserService userService,
            IGeoLocationService geoLocationService,
            IBotService botService) : base(applicationDbContext, mapper)
        {
            _hubContext = hubContext;
            _userService = userService;
            _geoLocationService = geoLocationService;
            _botService = botService;
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

        public async Task SendGameCreatedMessage(GameCreatedModel model)
        {
            await _hubContext.Clients.Users(new List<string>() { model.HostUserId, model.GuestUserId })
            .SendAsync("GameCreated", JsonConvert.SerializeObject(model));

            if (model.IsBotActivated)
            {
                var botUser = _userService.GetUserByUserId(model.GuestUserId);
                var credentials = await _userService.Login(new Services.Models.LoginModel() { Username = botUser.Nickname, Password = "N123456w!" });
                await BotSendsAnswer(model, credentials.AccessToken);
            }            
        }

        private async Task BotSendsAnswer(GameCreatedModel model, string accessToken)
        {
            Random random = new Random();
            var delay = random.Next(3, 10);

            await Task.Delay(TimeSpan.FromSeconds(delay));

            var choice = random.Next(1, 3);
            var randomChoice = (Enum.RockPaperScissorsType)System.Enum.ToObject(typeof(Enum.RockPaperScissorsType), choice);

            await _botService.SendAnswer(accessToken, new SendChoiceViewModel() { Choice = randomChoice, GameId = model.GameId, GameRoundId = model.GameRoundId, Round = model.Round });
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
