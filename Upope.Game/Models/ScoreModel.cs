namespace Upope.Game.Models
{
    public class ScoreModel
    {
        public ScoreModel(string userId, int score)
        {
            UserId = userId;
            Score = score;
        }
        public int Score { get; set; }
        public string UserId { get; set; }        
    }
}
