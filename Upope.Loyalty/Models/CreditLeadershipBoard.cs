
namespace Upope.Loyalty.Models
{
    public class CreditLeadershipBoard: LeadershipBoardBase
    {
        public CreditLeadershipBoard(int creditRecord, string userId, string username, string imagePath):base(userId, username, imagePath)
        {
            CreditRecord = creditRecord;
            UserId = userId;
            Username = username;
            ImagePath = imagePath;
        }

        public int CreditRecord { get; set; }
    }
}
