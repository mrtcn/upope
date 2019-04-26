namespace Upope.Game.Services.Models
{
    public class GameCreatedModel
    {
        public GameCreatedModel(int gameId, string hostUserId, string guestUserId)
        {
            GameId = gameId;
            HostUserId = hostUserId;
            GuestUserId = guestUserId;
        }
        public int GameId { get; set; }
        public string HostUserId { get; set; }
        public string GuestUserId { get; set; }
    }
}
