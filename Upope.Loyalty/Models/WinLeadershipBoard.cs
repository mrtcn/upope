
namespace Upope.Loyalty.Models
{
    public class WinLeadershipBoard: LeadershipBoardBase
    {
        public WinLeadershipBoard(int winRecord, string userId, string username, string imagePath) : base(userId, username, imagePath)
        {
            WinRecord = winRecord;
            UserId = userId;
            Username = username;
            ImagePath = imagePath;
        }

        public int WinRecord { get; set; }
    }
}
