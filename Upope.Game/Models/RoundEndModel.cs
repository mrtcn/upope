
namespace Upope.Game.Models
{
    public class RoundEndModel
    {
        public RoundEndModel()
        {

        }

        public RoundEndModel(int gameId)
        {
            GameId = gameId;
        }
        public int GameId { get; set; }
        public int GameRoundId { get; set; }
        public int Round { get; set; }
    }
}
