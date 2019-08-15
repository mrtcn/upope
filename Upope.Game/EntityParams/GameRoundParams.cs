using System;
using Upope.Game.Data.Entities;
using Upope.Game.Enum;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.Models;

namespace Upope.Game.EntityParams
{
    public class GameRoundParams : IEntityParams, IGameRound, IOperatorFields
    {
        public GameRoundParams(int gameId, int gameRoundId, int round)
        {
            Id = gameRoundId;
            GameId = gameId;
            Round = round;
        }

        public GameRoundParams(int gameId, int round)
        {
            Id = 0;
            GameId = gameId;
            Round = round;
        }

        public GameRoundParams(int gameId)
        {
            Id = 0;
            GameId = gameId;
        }

        public int Id { get; set; }
        public int GameId { get; set; }
        public int Round { get; set; }
        public Status Status { get; set; }
        public string WinnerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
