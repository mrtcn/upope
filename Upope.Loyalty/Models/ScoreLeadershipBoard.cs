
namespace Upope.Loyalty.Models
{
    public class ScoreLeadershipBoard: LeadershipBoardBase
    {
        public ScoreLeadershipBoard(int scoreRecord, string userId, string username, string imagePath) : base(userId, username, imagePath)
        {
            ScoreRecord = scoreRecord;
            UserId = userId;
            Username = username;
            ImagePath = imagePath;
        }

        public int ScoreRecord { get; set; }
    }
}
