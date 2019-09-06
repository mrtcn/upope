
namespace Upope.Loyalty.ViewModels
{
    public class CreditLeadershipBoardViewModel : LeadershipBoardBaseViewModel
    {
        public CreditLeadershipBoardViewModel(int creditRecord, string userId, string username, string imagePath): base(userId, username, imagePath)
        {
            CreditRecord = creditRecord;
            UserId = userId;
            Username = username;
            ImagePath = imagePath;
        }

        public int CreditRecord { get; set; }
    }
}
