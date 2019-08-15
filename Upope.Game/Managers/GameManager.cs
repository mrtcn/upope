﻿using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
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
        private readonly IMapper _mapper;
        private readonly IHubContext<GameHubs> _hubContext;

        public GameManager(
            IStringLocalizer<GameManager> localizer,
            IRoundAnswerService roundAnswerService,
            IGameRoundService gameRoundService,
            IBluffService bluffService,
            IGameService gameService,
            ILoyaltySyncService loyaltySyncService,
            IMapper mapper,
            IHubContext<GameHubs> hubContext)
        {
            _localizer = localizer;
            _roundAnswerService = roundAnswerService;
            _gameRoundService = gameRoundService;
            _bluffService = bluffService;
            _gameService = gameService;
            _loyaltySyncService = loyaltySyncService;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public GameRoundParams CreateOrUpdateGame(CreateOrUpdateViewModel model)
        {
            var gameParams = _mapper.Map<CreateOrUpdateViewModel, GameParams>(model);
            _gameService.CreateOrUpdate(gameParams);

            var gameRoundParams = new GameRoundParams(gameParams.Id);
            gameRoundParams.Round = gameRoundParams.Id == 0 ? 1 : gameRoundParams.Round;

            _gameRoundService.CreateOrUpdate(gameRoundParams);
            _gameService.SendGameCreatedMessage(new Services.Models.GameCreatedModel(gameParams.Id, gameParams.HostUserId, gameParams.GuestUserId));

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

                await _loyaltySyncService.AddCredit(new CreditsViewModel(gameParams.WinnerId, gameEndResult.TotalPoints), accessToken);
                await _loyaltySyncService.ChargeCredit(new CreditsViewModel(loserId, game.Credit), accessToken);

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
    }
}