
namespace Upope.Game.Services.Models
{
    public class WinModel
    {
        public WinModel(string userId, bool roundWin, bool gameWin)
        {
            UserId = userId;
            RoundWin = roundWin;
            GameWin = gameWin;
        }

        public string UserId { get; set; }
        public bool RoundWin { get; set; }
        public bool GameWin { get; set; }
    }
}
