
using Upope.Game.Enum;

namespace Upope.Game.EntityParams
{
    public class GameResult
    {
        public GameResult()
        {

        }

        public GameResult(
            string winnerId,
            int winningCount,
            int winningScore,
            int defeatCount,
            int defeatScore,
            int bluffCount,
            int bluffScore,
            int superBluffCount,
            int superBluffScore,
            int score,
            int winnerCredit,
            int loserCredit)
        {
            WinnerId = winnerId;
            WinningCount = winningCount;
            WinningScore = winningScore;
            DefeatCount = defeatCount;
            DefeatScore = defeatScore;
            BluffCount = bluffCount;
            BluffScore = bluffScore;
            SuperBluffCount = superBluffCount;
            SuperBluffScore = superBluffScore;
            Score = score;
            WinnerCredit = winnerCredit;
            LoserCredit = loserCredit;
        }

        public string WinnerId { get; set; }
        public int WinningCount { get; set; }
        public int WinningScore { get; set; }
        public int DefeatCount { get; set; }
        public int DefeatScore { get; set; }
        public int BluffCount { get; set; }
        public int BluffScore { get; set; }
        public int SuperBluffCount { get; set; }
        public int SuperBluffScore { get; set; }
        public int Score { get; set; }
        public int WinnerCredit { get; set; }
        public int LoserCredit { get; set; }
    }
}
