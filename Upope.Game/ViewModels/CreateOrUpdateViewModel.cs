namespace Upope.Game.ViewModels
{
    public class CreateOrUpdateViewModel
    {
        public CreateOrUpdateViewModel(int gameId, int gameRoundId, int round, string hostUserId, string guestUserId, int credit)
        {
            GameId = gameId;
            GameRoundId = gameRoundId;
            Round = round;
            HostUserId = hostUserId;
            GuestUserId = guestUserId;
            Credit = credit;
        }

        public int GameId { get; set; }
        public int GameRoundId { get; set; }
        public int Round { get; set; }
        public string HostUserId { get; set; }
        public string GuestUserId { get; set; }
        public int Credit { get; set; }
    }
}
