using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upope.Challenge.Hubs;
using Upope.Game.CustomException;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.Game.Enum;
using Upope.Game.GlobalSettings;
using Upope.Game.Services.Interfaces;
using Upope.Game.Services.Models;
using Upope.Game.ViewModels;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Models;

namespace Upope.Game.Services
{    
    public class GameRoundService : EntityServiceBase<GameRound>, IGameRoundService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IPointService _pointService;
        private readonly IBluffService _bluffService;
        private readonly IGameService _gameService;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private readonly IHubContext<GameHubs> _hubContext;

        public GameRoundService(
            ApplicationDbContext applicationDbContext,
            IPointService pointService,
            IBluffService bluffService,
            IGameService gameService,
            IIdentityService identityService,
            IMapper mapper,
            IHubContext<GameHubs> hubContext) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _pointService = pointService;
            _bluffService = bluffService;
            _gameService = gameService;
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
                var gameResult = entityParams as GameRoundParams;
                gameResult.Round = lastRound++;
            }            
        }

        public GameRoundParams GetLatestRound(int gameId, string userId)
        {
            //var isHost = Entities.Include(x => x.Game).FirstOrDefault(x => x.GameId == gameId && x.Game.HostUserId == userId) != null;
            var latestGameRound = Entities.Where(x => x.GameId == gameId).OrderByDescending(x => x.Round).FirstOrDefault();
            var gameRoundParams = _mapper.Map<GameRound, GameRoundParams>(latestGameRound);

            return gameRoundParams;
        }

        public async Task<GameRoundParams> SendChoice(SendChoiceParams sendChoiceParams)
        {
            var lastRoundEntity = Entities.Include(x => x.Game)
                .LastOrDefault(
                x => x.GameId == sendChoiceParams.GameId
                && x.Status == Status.Active);

            var isHost = false;

            if(lastRoundEntity.Game.HostUserId == sendChoiceParams.UserId)
            {
                lastRoundEntity.HostAnswer = sendChoiceParams.Choice;
                isHost = true;
            }                
            else
                lastRoundEntity.GuestAnswer = sendChoiceParams.Choice;

            var gameRoundParams = _mapper.Map<GameRound, GameRoundParams>(lastRoundEntity);

            if (gameRoundParams.HostAnswer != RockPaperScissorsType.NotAnswered && gameRoundParams.GuestAnswer != RockPaperScissorsType.NotAnswered)
            {
                var winModel = WinnerModel(
                    lastRoundEntity.GameId, 
                    lastRoundEntity.HostAnswer, 
                    lastRoundEntity.Game.HostUserId, 
                    lastRoundEntity.GuestAnswer, 
                    lastRoundEntity.Game.GuestUserId);
                gameRoundParams.WinnerId = winModel.UserId;
                CreateOrUpdate(gameRoundParams);

                var nextRound = lastRoundEntity != null ? lastRoundEntity.Round++ : 0;

                var winner = await _identityService.GetUserProfile(sendChoiceParams.AccessToken, sendChoiceParams.UserId);
                var bluff = _bluffService.Entities.FirstOrDefault(x => x.GameRoundId == gameRoundParams.Id);
                var isBluff = bluff == null ? false : !bluff.IsSuperBluff;
                var isSuperBluff = bluff == null ? false : bluff.IsSuperBluff;

                var roundResult = new RoundResult(winModel.UserId, sendChoiceParams.Choice, winModel.ChoiceResultType, isBluff, isSuperBluff);

                if (!string.IsNullOrEmpty(winModel.UserId))
                {
                    await _hubContext.Clients
                    .Users(new List<string>() { lastRoundEntity.Game.HostUserId, lastRoundEntity.Game.GuestUserId })
                    .SendAsync("RoundEnds", roundResult);

                    if (!winModel.GameWin)
                    {
                        var nextGameResult = new GameRoundParams(lastRoundEntity.GameId, nextRound);
                        CreateOrUpdate(nextGameResult);

                        await _hubContext.Clients
                        .Users(new List<string>() { lastRoundEntity.Game.HostUserId, lastRoundEntity.Game.GuestUserId })
                        .SendAsync("RoundEnds", roundResult);
                    }
                    else
                    {
                        var isWinnerHost = winModel.UserId == lastRoundEntity.Game.HostUserId;
                        var gameScore = _pointService.CalculatePoints(lastRoundEntity.GameId, isWinnerHost);

                        await _hubContext.Clients
                        .Users(new List<string>() { lastRoundEntity.Game.HostUserId, lastRoundEntity.Game.GuestUserId })
                        .SendAsync("GameEnds", gameScore);
                    }
                }                
            }
            else if (sendChoiceParams.Choice == RockPaperScissorsType.NotAnswered)
            {
                var hostWin = Entities.Where(x => x.GameId == lastRoundEntity.Game.Id && x.WinnerId == lastRoundEntity.Game.HostUserId).Count();
                var guestWin = Entities.Where(x => x.GameId == lastRoundEntity.Game.Id && x.WinnerId == lastRoundEntity.Game.GuestUserId).Count();

                if(hostWin == AppSettingsProvider.WinRoundCount || guestWin == AppSettingsProvider.WinRoundCount)
                {
                    var gameScore = _pointService.CalculatePoints(lastRoundEntity.GameId);

                    await _hubContext.Clients
                       .Users(new List<string>() { lastRoundEntity.Game.HostUserId, lastRoundEntity.Game.GuestUserId })
                       .SendAsync("GameEnds", gameScore);
                }
                else
                {
                    var bluff = _bluffService.Entities.FirstOrDefault(x => x.GameRoundId == gameRoundParams.Id);
                    var isBluff = bluff == null ? false : !bluff.IsSuperBluff;
                    var isSuperBluff = bluff == null ? false : bluff.IsSuperBluff;

                    var roundResult = new RoundResult(
                    isHost ? lastRoundEntity.Game.GuestUserId : lastRoundEntity.Game.HostUserId,
                    sendChoiceParams.Choice,
                    ChoiceResultType.NotAnswered,
                    isBluff,
                    isSuperBluff);

                    await _hubContext.Clients
                        .Users(new List<string>() { lastRoundEntity.Game.HostUserId, lastRoundEntity.Game.GuestUserId })
                        .SendAsync("RoundEnds", roundResult);
                }
            }
            else
            {
                CreateOrUpdate(gameRoundParams);
            }

            return gameRoundParams;
        }

        public bool IsFirstAnswer(GameRoundParams gameRoundParams)
        {
            if (gameRoundParams.GuestAnswer == RockPaperScissorsType.NotAnswered || gameRoundParams.HostAnswer == RockPaperScissorsType.NotAnswered)
            {
                return true;
            }

            return false;
        }

        private WinModel WinnerModel(int id, RockPaperScissorsType hostChoice, string hostUserId, RockPaperScissorsType guestChoice, string guestUserId)
        {
            var hostWin = Entities.Where(x => x.GameId == id && x.WinnerId == hostUserId).Count();
            var guestWin = Entities.Where(x => x.GameId == id && x.WinnerId == guestUserId).Count();
            var userId = string.Empty;

            if (hostChoice == guestChoice)
                return new WinModel(userId, false, false, ChoiceResultType.Draw);

            if (hostChoice == RockPaperScissorsType.Paper && guestChoice == RockPaperScissorsType.Scissors){
                return new WinModel(guestUserId, true, (guestWin == AppSettingsProvider.WinRoundCount), ChoiceResultType.ScissorCutsPaper);
            } else if (hostChoice == RockPaperScissorsType.Rock && guestChoice == RockPaperScissorsType.Paper)
            {
                return new WinModel(guestUserId, true, (guestWin == AppSettingsProvider.WinRoundCount), ChoiceResultType.PaperCoversRock);
            } else if (hostChoice == RockPaperScissorsType.Scissors && guestChoice == RockPaperScissorsType.Rock)
            {
                return new WinModel(guestUserId, true, (guestWin == AppSettingsProvider.WinRoundCount), ChoiceResultType.RockBreaksScissor);
            } else if(hostChoice == RockPaperScissorsType.NotAnswered)
            {
                return new WinModel(guestUserId, true, (guestWin == AppSettingsProvider.WinRoundCount), ChoiceResultType.NotAnswered);
            }

            if (guestChoice == RockPaperScissorsType.Paper && hostChoice == RockPaperScissorsType.Scissors)
            {
                return new WinModel(hostUserId, true, (hostWin == AppSettingsProvider.WinRoundCount), ChoiceResultType.ScissorCutsPaper);
            }
            else if (guestChoice == RockPaperScissorsType.Rock && hostChoice == RockPaperScissorsType.Paper)
            {
                return new WinModel(hostUserId, true, (hostWin == AppSettingsProvider.WinRoundCount), ChoiceResultType.PaperCoversRock);
            }
            else if (guestChoice == RockPaperScissorsType.Scissors && hostChoice == RockPaperScissorsType.Rock)
            {
                return new WinModel(hostUserId, true, (hostWin == AppSettingsProvider.WinRoundCount), ChoiceResultType.RockBreaksScissor);
            }
            else if (guestChoice == RockPaperScissorsType.NotAnswered)
            {
                return new WinModel(hostUserId, true, (hostWin == AppSettingsProvider.WinRoundCount), ChoiceResultType.NotAnswered);
            }

            throw new WrongChoiceTypeException("Given Choice is not valid");
        }

        public async Task AskBluff(string userId, GameRoundParams gameRoundParams)
        {
            if (IsFirstAnswer(gameRoundParams))
            {
                var isHost = _gameService.IsHostUser(gameRoundParams.GameId, userId);
                var game = _gameService.Get(gameRoundParams.GameId);
                var askBluffUserId = isHost ? game.GuestUserId : game.HostUserId;
                await _hubContext.Clients.User(askBluffUserId).SendAsync("AskBluff");
            }
        }

        public async Task TextBluff(SendBluffViewModel model, string userId)
        {
            var isHostUser = _gameService.IsHostUser(model.GameId, userId);

            var game = _gameService.Get(model.GameId);
            var textBluffUserId = isHostUser ? game.GuestUserId : game.HostUserId;
            await _hubContext.Clients.User(textBluffUserId).SendAsync("TextBluff");
        }
    }
}
