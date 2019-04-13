
namespace Upope.Game.EntityParams
{
    public class RoundWinnerParams
    {
        public RoundWinnerParams()
        {

        }

        public RoundWinnerParams (int gameId, string winnerId, string winnerName, int round){
            GameId = gameId;
            WinnerId = winnerId;
            WinnerName = winnerName;
            Round = round;
        }

        public int GameId { get; set; }
        public string WinnerId { get; set; }
        public string WinnerName { get; set; }
        public int Round { get; set; }
    }
}
