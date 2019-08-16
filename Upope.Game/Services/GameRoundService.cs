using AutoMapper;
using System.Linq;
using Upope.Game.CustomException;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.Game.Enum;
using Upope.Game.GlobalSettings;
using Upope.Game.Services.Interfaces;
using Upope.Game.Services.Models;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;

namespace Upope.Game.Services
{    
    public class GameRoundService : EntityServiceBase<GameRound>, IGameRoundService
    {
        private readonly IPointService _pointService;
        private readonly IBluffService _bluffService;
        private readonly IRoundAnswerService _roundAnswerService;
        private readonly IGameService _gameService;

        public GameRoundService(
            ApplicationDbContext applicationDbContext,
            IPointService pointService,
            IBluffService bluffService,
            IRoundAnswerService roundAnswerService,
            IGameService gameService,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _pointService = pointService;
            _bluffService = bluffService;
            _roundAnswerService = roundAnswerService;
            _gameService = gameService;
        }

        public bool IsRoundOver(int gameRoundId)
        {
            var roundCount = Entities.Where(x => x.Status == Status.Active && x.Id == gameRoundId)
                .SelectMany(x => x.RoundAnswers)
                .Where(x => x.Status == Status.Active).Count();

            return roundCount > 1;
        }

        public bool IsGameOver(int gameId)
        {
            var game = _gameService.Get(gameId);
            var hostUserWinCount = Entities.Where(x => x.Status == Status.Active && x.GameId == gameId && x.WinnerId == game.HostUserId).Count();

            if (hostUserWinCount == AppSettingsProvider.WinRoundCount)
                return true;

            var guestUserWinCount = Entities.Where(x => x.Status == Status.Active && x.GameId == gameId && x.WinnerId == game.GuestUserId).Count();

            return AppSettingsProvider.WinRoundCount == guestUserWinCount;
        }

        public string GameWinnerId(int gameId)
        {
            var game = _gameService.Get(gameId);
            var hostUserWinCount = Entities.Where(x => x.Status == Status.Active && x.GameId == gameId && x.WinnerId == game.HostUserId).Count();

            if (hostUserWinCount == AppSettingsProvider.WinRoundCount)
                return game.HostUserId;

            var guestUserWinCount = Entities.Where(x => x.Status == Status.Active && x.GameId == gameId && x.WinnerId == game.GuestUserId).Count();

            return AppSettingsProvider.WinRoundCount == guestUserWinCount ? game.GuestUserId : string.Empty;
        }


        public RoundResult CalculateRoundEnd(int gameRoundId)
        {
            var bluff = _bluffService.Entities.FirstOrDefault(x => x.GameRoundId == gameRoundId);
            var isBluff = bluff == null ? false : !bluff.IsSuperBluff;
            var isSuperBluff = bluff == null ? false : bluff.IsSuperBluff;

            var roundAnswers = _roundAnswerService.RoundAnswers(gameRoundId);
            var hostUserId = roundAnswers.FirstOrDefault().UserId;
            var guestUserId = roundAnswers.LastOrDefault().UserId;

            var winModel = WinnerModel(
                    roundAnswers.FirstOrDefault().Choice,
                    hostUserId,
                    roundAnswers.LastOrDefault().Choice,
                    guestUserId);

            var roundResult = new RoundResult(winModel.UserId, winModel.ChoiceResultType, isBluff, isSuperBluff);

            return roundResult;
        }

        public GameScore CalculateGameEnd(int gameId)
        {
            var winnerId = GameWinnerId(gameId);
            var gameScore = _pointService.CalculatePoints(gameId, winnerId);

            return gameScore;
        }

        private WinModel WinnerModel(RockPaperScissorsType hostChoice, string hostUserId, RockPaperScissorsType guestChoice, string guestUserId)
        {
            if (hostChoice == guestChoice)
                return new WinModel(string.Empty, ChoiceResultType.Draw);

            if (hostChoice == RockPaperScissorsType.Paper && guestChoice == RockPaperScissorsType.Scissors){
                return new WinModel(guestUserId, ChoiceResultType.ScissorCutsPaper);
            } else if (hostChoice == RockPaperScissorsType.Rock && guestChoice == RockPaperScissorsType.Paper)
            {
                return new WinModel(guestUserId, ChoiceResultType.PaperCoversRock);
            } else if (hostChoice == RockPaperScissorsType.Scissors && guestChoice == RockPaperScissorsType.Rock)
            {
                return new WinModel(guestUserId, ChoiceResultType.RockBreaksScissor);
            } else if(hostChoice == RockPaperScissorsType.NotAnswered)
            {
                return new WinModel(guestUserId, ChoiceResultType.NotAnswered);
            }

            if (guestChoice == RockPaperScissorsType.Paper && hostChoice == RockPaperScissorsType.Scissors)
            {
                return new WinModel(hostUserId, ChoiceResultType.ScissorCutsPaper);
            }
            else if (guestChoice == RockPaperScissorsType.Rock && hostChoice == RockPaperScissorsType.Paper)
            {
                return new WinModel(hostUserId, ChoiceResultType.PaperCoversRock);
            }
            else if (guestChoice == RockPaperScissorsType.Scissors && hostChoice == RockPaperScissorsType.Rock)
            {
                return new WinModel(hostUserId, ChoiceResultType.RockBreaksScissor);
            }
            else if (guestChoice == RockPaperScissorsType.NotAnswered)
            {
                return new WinModel(hostUserId, ChoiceResultType.NotAnswered);
            }

            throw new WrongChoiceTypeException("Given Choice is not valid");
        }
    }
}
