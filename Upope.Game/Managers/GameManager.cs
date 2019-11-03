using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upope.Game.CustomException;
using Upope.Game.EntityParams;
using Upope.Game.Hubs;
using Upope.Game.Interfaces;
using Upope.Game.Models;
using Upope.Game.Services.Interfaces;
using Upope.Game.ViewModels;

namespace Upope.Game.Managers
{
    public class GameManager : IGameManager
    {
        private readonly IStringLocalizer<GameManager> _localizer;
        private readonly IRoundAnswerService _roundAnswerService;
        private readonly IGameRoundService _gameRoundService;
        private readonly IBluffService _bluffService;
        private readonly IGameService _gameService;
        private readonly ILoyaltySyncService _loyaltySyncService;
        private readonly IBotService _botService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHubContext<GameHub> _hubContext;

        public GameManager(
            IStringLocalizer<GameManager> localizer,
            IRoundAnswerService roundAnswerService,
            IGameRoundService gameRoundService,
            IBluffService bluffService,
            IGameService gameService,
            ILoyaltySyncService loyaltySyncService,
            IBotService botService,
            IUserService userService,
            IMapper mapper,
            IHubContext<GameHub> hubContext)
        {
            _localizer = localizer;
            _roundAnswerService = roundAnswerService;
            _gameRoundService = gameRoundService;
            _bluffService = bluffService;
            _gameService = gameService;
            _loyaltySyncService = loyaltySyncService;
            _botService = botService;
            _userService = userService;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public void SendRematch(string userId, string requestingUserId, int credit, int maxCredit)
        {
            var rematchUserInfo = _gameService.RematchUserInfo(userId, requestingUserId, credit, maxCredit);
            _hubContext.Clients.User(userId).SendAsync("RematchRequest", rematchUserInfo);
        }

        public void RejectRematch(string userId, string requestingUserId)
        {
            _hubContext.Clients.User(userId).SendAsync("RejectRematch", new { UserId = requestingUserId});
        }

        public void SendRematchRaise(string userId, string requestingUserId, int credit, int maxCredit)
        {
            var rematchUserInfo = _gameService.RematchUserInfo(userId, requestingUserId, credit, maxCredit);
            _hubContext.Clients.User(userId).SendAsync("RematchRaiseRequest", rematchUserInfo);
        }

        public GameRoundParams CreateOrUpdateGame(CreateOrUpdateViewModel model)
        {
            var gameParams = _mapper.Map<CreateOrUpdateViewModel, GameParams>(model);
            _gameService.CreateOrUpdate(gameParams);

            var gameRoundParams = new GameRoundParams(gameParams.Id);
            gameRoundParams.Round = gameRoundParams.Id == 0 ? 1 : gameRoundParams.Round;

            var gameRound = _gameRoundService.CreateOrUpdate(gameRoundParams);
            _gameService.SendGameCreatedMessage(new Services.Models.GameCreatedModel(gameParams.Id, gameParams.HostUserId, gameParams.GuestUserId, gameRound.Id, gameRound.Round, model.IsBotActivated));

            return gameRoundParams;
        }

        public async Task<bool> SendBluff(string userId, SendBluffViewModel model)
        {            
            var roundAnswers = _roundAnswerService.RoundAnswers(model.GameRoundId);
            if (roundAnswers.All(x => x.Choice != Enum.RockPaperScissorsType.NotAnswered))
            {
                return false;
            }

            BluffParams bluffParams = _bluffService.GetBluffParams(model, userId, model.Choice);
            _bluffService.CreateOrUpdate(bluffParams);
            await _bluffService.TextBluff(model, userId);

            return true;
        }

        public async Task<RoundEndModel> SendChoice(SendChoiceViewModel model, string userId, string accessToken)
        {
            var roundEndModel = new RoundEndModel(model.GameId);

            var answerCount = _roundAnswerService.RoundAnswerCount(model.GameRoundId, userId);
            if (answerCount > 1)
                throw new AlreadyAnsweredException(_localizer.GetString("AlreadyAnswered").Value);

            var roundAnswerEntity = _roundAnswerService.CreateOrUpdate(
                new RoundAnswerParams()
                {
                    Choice = model.Choice,
                    GameRoundId = model.GameRoundId,
                    UserId = userId
                });

            if (_gameRoundService.IsRoundOver(model.GameRoundId))
            {
                var roundEnd = _gameRoundService.CalculateRoundEnd(model.GameRoundId);

                var gameRound = _gameRoundService.Get(model.GameRoundId);
                var gameRoundParams = _mapper.Map<GameRoundParams>(gameRound);
                gameRoundParams.WinnerId = roundEnd.WinnerId;

                _gameRoundService.CreateOrUpdate(gameRoundParams);

                var game = _gameService.Get(model.GameId);

                if (!_gameRoundService.IsGameOver(model.GameId))
                {
                    var newRound = model.Round + 1;
                    await _hubContext.Clients
                        .Users(new List<string>() { game.HostUserId, game.GuestUserId })
                        .SendAsync("RoundEnds", JsonConvert.SerializeObject(roundEnd));

                    var newGameRoundEntity = _gameRoundService.CreateOrUpdate(new GameRoundParams(model.GameId, 0, newRound));
                    roundEndModel.Round = newRound;
                    roundEndModel.GameRoundId = newGameRoundEntity.Id;

                    
                    if (game.IsBotActivated)
                    {
                        var botUser = _userService.GetUserByUserId(game.GuestUserId);
                        var credentials = await _userService.Login(new Services.Models.LoginModel() { Username = botUser.Nickname, Password = "N123456w!" });

                        await BotSendAnswer(game, newGameRoundEntity, credentials.AccessToken);
                    }
                    return roundEndModel;
                }
            }

            if (_gameRoundService.IsGameOver(model.GameId))
            {
                var gameEndResult = _gameRoundService.CalculateGameEnd(model.GameId);
                var game = _gameService.Get(model.GameId);
                var gameParams = _mapper.Map<GameParams>(game);
                gameParams.WinnerId = gameEndResult.UserId;
                var loserId = gameParams.WinnerId == game.HostUserId ? game.GuestUserId : game.HostUserId;
                _gameService.CreateOrUpdate(gameParams);

                var totalCreditsToAdd = game.Credit + gameEndResult.GainedCredits;

                await _loyaltySyncService.AddCredit(new CreditsModel(gameParams.WinnerId, totalCreditsToAdd), accessToken);
                await _loyaltySyncService.ChargeCredit(new CreditsModel(loserId, game.Credit), accessToken);

                await _loyaltySyncService.AddScores(new ScoreModel(gameParams.WinnerId, gameEndResult.TotalPoints), accessToken);

                if (!game.IsRematch)
                {
                    await _loyaltySyncService.AddWin(gameParams.WinnerId, accessToken);
                    await _loyaltySyncService.ResetWin(loserId, accessToken);
                }

                await _hubContext.Clients
                .Users(new List<string>() { game.HostUserId, game.GuestUserId })
                .SendAsync("GameEnds", JsonConvert.SerializeObject(gameEndResult));
            }            
            else
            {
                await _bluffService.AskBluff(userId, model.GameId, model.GameRoundId);
            }

            roundEndModel.Round = model.Round;
            roundEndModel.GameRoundId = roundAnswerEntity.GameRoundId;
            return roundEndModel;
        }

        private async Task BotSendAnswer(Data.Entities.Game game, Data.Entities.GameRound newGameRoundEntity, string accessToken)
        {
            if (game.IsBotActivated)
            {
                Random random = new Random();
                var delay = random.Next(3, 10);

                await Task.Delay(TimeSpan.FromSeconds(delay));

                var choice = random.Next(1, 3);
                var randomChoice = (Enum.RockPaperScissorsType)System.Enum.ToObject(typeof(Enum.RockPaperScissorsType), choice);
                await _botService.SendAnswer(accessToken, new SendChoiceViewModel() { Choice = randomChoice, GameId = game.Id, GameRoundId = newGameRoundEntity.Id, Round = newGameRoundEntity.Round });
            }
        }
    }
}
