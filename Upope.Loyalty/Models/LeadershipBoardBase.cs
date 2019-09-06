
namespace Upope.Loyalty.Models
{
    public class LeadershipBoardBase
    {
        public LeadershipBoardBase(string userId, string username, string imagePath)
        {
            UserId = userId;
            Username = username;
            ImagePath = imagePath;
        }

        public int Position { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string ImagePath { get; set; }
        public string Country { get; set; }
    }
}
