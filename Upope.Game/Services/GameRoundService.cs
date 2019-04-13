using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upope.Challenge.Hubs;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.Game.Enum;
using Upope.Game.GlobalSettings;
using Upope.Game.Services.Interfaces;
using Upope.Game.Services.Models;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Models;

namespace Upope.Game.Services
{    
    public class GameRoundService : EntityServiceBase<GameRound>, IGameRoundService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private readonly IHubContext<GameHubs> _hubContext;

        public GameRoundService(
            ApplicationDbContext applicationDbContext,
            IIdentityService identityService,
            IMapper mapper,
            IHubContext<GameHubs> hubContext) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _identityService = identityService;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        protected override void OnSaveChanges(IEntityParams entityParams, GameRound entity)
        {
            base.OnSaveChanges(entityParams, entity);

            if(entity.Id == 0)
            {
                var lastRoundEntity = Entities.LastOrDefault(
                x => x.GameId == entity.GameId
                && x.Status == Status.Active
                && x.GuestAnswer != RockPaperScissorsType.NotAnswered
                && x.HostAnswer != RockPaperScissorsType.NotAnswered);

                var lastRound = lastRoundEntity != null ? lastRoundEntity.Round : 0;

                entity.Round = lastRound++;
                var gameRoundParams = entityParams as GameRoundParams;
                gameRoundParams.Round = lastRound++;
            }            
        }

        public async Task SendChoice(SendChoiceParams sendChoiceParams)
        {
            var lastRoundEntity = Entities.Include(x => x.Game)
                .LastOrDefault(
                x => x.GameId == sendChoiceParams.GameId
                && x.Status == Status.Active);

            if(lastRoundEntity.Game.HostUserId == sendChoiceParams.UserId)
                lastRoundEntity.HostAnswer = sendChoiceParams.Choice;
            else
                lastRoundEntity.GuestAnswer = sendChoiceParams.Choice;

            var gameRoundParams = _mapper.Map<GameRound, GameRoundParams>(lastRoundEntity);
            
            if (gameRoundParams.HostAnswer != RockPaperScissorsType.NotAnswered && gameRoundParams.GuestAnswer != RockPaperScissorsType.NotAnswered)
            {
                var winModel = WinnerId(lastRoundEntity.GameId, lastRoundEntity.HostAnswer, lastRoundEntity.Game.HostUserId, lastRoundEntity.GuestAnswer, lastRoundEntity.Game.GuestUserId);
                gameRoundParams.WinnerId = winModel.UserId;
                CreateOrUpdate(gameRoundParams);

                var nextRound = lastRoundEntity != null ? lastRoundEntity.Round++ : 0;

                var winner = await _identityService.GetUserProfile(sendChoiceParams.AccessToken, sendChoiceParams.UserId);
                var roundWinnerParams = new RoundWinnerParams(lastRoundEntity.GameId, winModel.UserId, winner.FirstName + " " + winner.LastName, nextRound);

                if (string.IsNullOrEmpty(winModel.UserId))
                {
                    await _hubContext.Clients
                    .Users(new List<string>() { lastRoundEntity.Game.HostUserId, lastRoundEntity.Game.GuestUserId })
                    .SendAsync("RoundWin", roundWinnerParams);

                    if (!winModel.GameWin)
                    {
                        var nextGameRoundParams = new GameRoundParams(lastRoundEntity.GameId, nextRound);
                        CreateOrUpdate(nextGameRoundParams);

                        await _hubContext.Clients
                        .Users(new List<string>() { lastRoundEntity.Game.HostUserId, lastRoundEntity.Game.GuestUserId })
                        .SendAsync("RoundWin", roundWinnerParams);
                    }
                    else
                    {
                        await _hubContext.Clients
                        .Users(new List<string>() { lastRoundEntity.Game.HostUserId, lastRoundEntity.Game.GuestUserId })
                        .SendAsync("GameWin", roundWinnerParams);
                    }
                }                
            }
            else
            {
                CreateOrUpdate(gameRoundParams);
            }            
        }

        private WinModel WinnerId(int id, RockPaperScissorsType hostChoice, string hostUserId, RockPaperScissorsType guestChoice, string guestUserId)
        {
            var hostWin = Entities.Where(x => x.GameId == id && x.WinnerId == hostUserId).Count();
            var guestWin = Entities.Where(x => x.GameId == id && x.WinnerId == guestUserId).Count();
            var userId = string.Empty;

            if (hostChoice == guestChoice)
                return new WinModel(userId, false, false);

            if ((hostChoice == RockPaperScissorsType.Paper && guestChoice == RockPaperScissorsType.Scissors)
                || (hostChoice == RockPaperScissorsType.Rock && guestChoice == RockPaperScissorsType.Paper)
                || (hostChoice == RockPaperScissorsType.Scissors && guestChoice == RockPaperScissorsType.Rock))
                return new WinModel(guestUserId, true, (hostWin == AppSettingsProvider.WinRoundCount || guestWin == AppSettingsProvider.WinRoundCount));

            return new WinModel(hostUserId, true, (hostWin == AppSettingsProvider.WinRoundCount));
        }
    }
}
