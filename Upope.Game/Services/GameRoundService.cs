using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upope.Game.CustomException;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.Game.Enum;
using Upope.Game.GlobalSettings;
using Upope.Game.Hubs;
using Upope.Game.Services.Interfaces;
using Upope.Game.Services.Models;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Models;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.Game.Services
{    
    public class GameRoundService : EntityServiceBase<GameRound>, IGameRoundService
    {
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
        }

        public GameRoundParams GetLastRoundEntity(int gameId)
        {
            //var isHost = Entities.Include(x => x.Game).FirstOrDefault(x => x.GameId == gameId && x.Game.HostUserId == userId) != null;
            var latestGameRound = Entities
                .Where(x => x.GameId == gameId && x.Status == Status.Active)
                .OrderByDescending(x => x.Round).FirstOrDefault();
            var gameRoundParams = _mapper.Map<GameRound, GameRoundParams>(latestGameRound);

            return gameRoundParams;
        }

        public async Task<GameRoundParams> SendChoice(SendChoiceParams sendChoiceParams)
        {
            var lastRoundEntity = Entities.Include(x => x.Game)
                .Where(x => x.GameId == sendChoiceParams.GameId && x.Status == Status.Active)
                .OrderByDescending(x => x.Round).FirstOrDefault();

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

                var nextRound = lastRoundEntity != null ? lastRoundEntity.Round + 1 : 1;

                var winner = await _identityService.GetUserProfileByAccessToken(sendChoiceParams.AccessToken, sendChoiceParams.UserId);
                var bluff = _bluffService.Entities.FirstOrDefault(x => x.GameRoundId == gameRoundParams.Id);
                var isBluff = bluff == null ? false : !bluff.IsSuperBluff;
                var isSuperBluff = bluff == null ? false : bluff.IsSuperBluff;

                var roundResult = new RoundResult(winModel.UserId, sendChoiceParams.Choice, winModel.ChoiceResultType, isBluff, isSuperBluff);

                if (!string.IsNullOrEmpty(winModel.UserId))
                {
                    if (!winModel.GameWin)
                    {
                        var nextGameResult = new GameRoundParams(lastRoundEntity.GameId, nextRound);
                        CreateOrUpdate(nextGameResult);

                        await _hubContext.Clients
                        .Users(new List<string>() { lastRoundEntity.Game.HostUserId, lastRoundEntity.Game.GuestUserId })
                        .SendAsync("RoundEnds", JsonConvert.SerializeObject(roundResult));
                    }
                    else
                    {
                        var isWinnerHost = winModel.UserId == lastRoundEntity.Game.HostUserId;
                        var gameScore = _pointService.CalculatePoints(lastRoundEntity.GameId, isWinnerHost);

                        var game = _gameService.Get(lastRoundEntity.GameId);
                        var gameParams = _mapper.Map<GameParams>(game);
                        gameParams.WinnerId = isWinnerHost ? gameParams.HostUserId : gameParams.GuestUserId;
                        _gameService.CreateOrUpdate(gameParams);

                        await _hubContext.Clients
                        .Users(new List<string>() { lastRoundEntity.Game.HostUserId, lastRoundEntity.Game.GuestUserId })
                        .SendAsync("GameEnds", JsonConvert.SerializeObject(gameScore));
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

                    var game = _gameService.Get(lastRoundEntity.GameId);
                    var gameParams = _mapper.Map<GameParams>(game);
                    gameParams.WinnerId = hostWin == AppSettingsProvider.WinRoundCount ? gameParams.HostUserId : gameParams.GuestUserId;
                    _gameService.CreateOrUpdate(gameParams);

                    await _hubContext.Clients
                       .Users(new List<string>() { lastRoundEntity.Game.HostUserId, lastRoundEntity.Game.GuestUserId })
                       .SendAsync("GameEnds", JsonConvert.SerializeObject(gameScore));
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
                        .SendAsync("RoundEnds", JsonConvert.SerializeObject(roundResult));
                }
            }
            else
            {
                CreateOrUpdate(gameRoundParams);
            }

            return gameRoundParams;
        }

        private WinModel WinnerModel(int id, RockPaperScissorsType hostChoice, string hostUserId, RockPaperScissorsType guestChoice, string guestUserId)
        {
            var hostWin = Entities.Where(x => x.GameId == id && x.WinnerId == hostUserId).Count();
            var guestWin = Entities.Where(x => x.GameId == id && x.WinnerId == guestUserId).Count();
            var userId = string.Empty;
            var winRoundCount = AppSettingsProvider.WinRoundCount;

            if (hostChoice == guestChoice)
                return new WinModel(userId, false, false, ChoiceResultType.Draw);

            if (hostChoice == RockPaperScissorsType.Paper && guestChoice == RockPaperScissorsType.Scissors){
                guestWin = guestWin + 1;
                return new WinModel(guestUserId, true, (guestWin == winRoundCount), ChoiceResultType.ScissorCutsPaper);
            } else if (hostChoice == RockPaperScissorsType.Rock && guestChoice == RockPaperScissorsType.Paper)
            {
                guestWin = guestWin + 1;
                return new WinModel(guestUserId, true, (guestWin == winRoundCount), ChoiceResultType.PaperCoversRock);
            } else if (hostChoice == RockPaperScissorsType.Scissors && guestChoice == RockPaperScissorsType.Rock)
            {
                guestWin = guestWin + 1;
                return new WinModel(guestUserId, true, (guestWin == winRoundCount), ChoiceResultType.RockBreaksScissor);
            } else if(hostChoice == RockPaperScissorsType.NotAnswered)
            {
                guestWin = guestWin + 1;
                return new WinModel(guestUserId, true, (guestWin == winRoundCount), ChoiceResultType.NotAnswered);
            }

            if (guestChoice == RockPaperScissorsType.Paper && hostChoice == RockPaperScissorsType.Scissors)
            {
                hostWin = hostWin + 1;
                return new WinModel(hostUserId, true, (hostWin == winRoundCount), ChoiceResultType.ScissorCutsPaper);
            }
            else if (guestChoice == RockPaperScissorsType.Rock && hostChoice == RockPaperScissorsType.Paper)
            {
                hostWin = hostWin + 1;
                return new WinModel(hostUserId, true, (hostWin == winRoundCount), ChoiceResultType.PaperCoversRock);
            }
            else if (guestChoice == RockPaperScissorsType.Scissors && hostChoice == RockPaperScissorsType.Rock)
            {
                hostWin = hostWin + 1;
                return new WinModel(hostUserId, true, (hostWin == winRoundCount), ChoiceResultType.RockBreaksScissor);
            }
            else if (guestChoice == RockPaperScissorsType.NotAnswered)
            {
                hostWin = hostWin + 1;
                return new WinModel(hostUserId, true, (hostWin == winRoundCount), ChoiceResultType.NotAnswered);
            }

            throw new WrongChoiceTypeException("Given Choice is not valid");
        }
    }
}
