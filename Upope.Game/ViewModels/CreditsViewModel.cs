
namespace Upope.Game.ViewModels
{
    public class CreditsViewModel
    {
        public CreditsViewModel(string userId, int credit)
        {
            UserId = userId;
            Credit = credit;
        }
        public int Credit { get; set; }
        public string UserId { get; set; }        
    }
}
