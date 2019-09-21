
namespace Upope.Loyalty.ViewModels
{
    public class ScoreLeadershipBoardViewModel : LeadershipBoardBaseViewModel
    {
        public ScoreLeadershipBoardViewModel(int scoreRecord, string userId, string username, string imagePath): base(userId, username, imagePath)
        {
            ScoreRecord = scoreRecord;
            UserId = userId;
            Username = username;
            ImagePath = imagePath;
        }

        public int ScoreRecord { get; set; }
    }
}
