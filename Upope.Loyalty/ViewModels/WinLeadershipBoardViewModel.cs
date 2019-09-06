
namespace Upope.Loyalty.ViewModels
{
    public class WinLeadershipBoardViewModel: LeadershipBoardBaseViewModel
    {
        public WinLeadershipBoardViewModel(int winRecord, string userId, string username, string imagePath): base(userId, username, imagePath)
        {
            WinRecord = winRecord;
            UserId = userId;
            Username = username;
            ImagePath = imagePath;
        }

        public int WinRecord { get; set; }
    }
}
