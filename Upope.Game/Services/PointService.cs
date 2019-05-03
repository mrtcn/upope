using Microsoft.EntityFrameworkCore;
using System.Linq;
using Upope.Game.Services.Interfaces;
using Upope.Game.Services.Models;
using Upope.ServiceBase.Enums;

namespace Upope.Game.Services
{
    public class PointService: IPointService
    {
        private readonly IGameService _gameService;
        private readonly IGameRoundService _gameRoundService;
        private readonly IBluffService _bluffService;

        public PointService(
            IGameService gameService,
            IGameRoundService gameRoundService,
            IBluffService bluffService)
        {
            _gameService = gameService;
            _gameRoundService = gameRoundService;
            _bluffService = bluffService;
        }
        public GameScore CalculatePoints(int gameId, bool? isWinnerHost = null)
        {
            var game = _gameService.Get(gameId);
            var gamePoint = game.Credit * 2;
            var hostId = game.HostUserId;
            var guestId = game.GuestUserId;
            var winnerId = isWinnerHost != null && isWinnerHost.GetValueOrDefault() ? hostId : guestId;

            var bluffAmount = _bluffService
                .Entities.Include(x => x.GameRound)
                .Where(x => x.GameRound.GameId == gameId && x.UserId == winnerId && x.IsSuperBluff == false && x.Status == Status.Active).Count();

            var superBluffAmount = _bluffService
                .Entities.Include(x => x.GameRound)
                .Where(x => x.GameRound.GameId == gameId && x.UserId == winnerId && x.IsSuperBluff && x.Status == Status.Active).Count();

            var loseAmount = _gameRoundService
                .Entities.Where(x => x.GameId == gameId && x.WinnerId == guestId && x.Status == Status.Active).Count();

            var winAmount = _gameRoundService
                .Entities.Where(x => x.GameId == gameId && x.WinnerId == winnerId && x.Status == Status.Active).Count();

            var bluffPoints = bluffAmount * gamePoint * 2;
            var superBluffPoints = superBluffAmount * gamePoint;
            var defeatlessPoints = loseAmount == 0 ? gamePoint : 0;
            var totalPoints = bluffPoints + superBluffPoints + defeatlessPoints;

            return new GameScore
            {
                UserId = winnerId,
                BluffAmount = bluffAmount,
                BluffPoints = bluffPoints,
                SuperBluffAmount = superBluffAmount,
                SuperBluffPoints = superBluffPoints,
                WinAmount = winAmount,
                LoseAmount = loseAmount,
                DefeatlessPoints = defeatlessPoints,
                TotalPoints = totalPoints,
                GainedCredits = totalPoints / 10
            };
        }
    }
}
