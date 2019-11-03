namespace Upope.Game.Services.Models
{
    public class GameCreatedModel
    {
        public GameCreatedModel(int gameId, string hostUserId, string guestUserId, int gameRoundId, int round, bool isBotActivated)
        {
            GameId = gameId;
            HostUserId = hostUserId;
            GuestUserId = guestUserId;
            GameRoundId = gameRoundId;
            IsBotActivated = isBotActivated;
            Round = round;
        }
        public int GameId { get; set; }
        public int GameRoundId { get; set; }
        public int Round { get; set; }
        public string HostUserId { get; set; }
        public string GuestUserId { get; set; }
        public bool IsBotActivated { get; set; }
    }
}
