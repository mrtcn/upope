
namespace Upope.Challenge.ViewModels
{
    public class FilterUsersViewModel
    {
        public FilterUsersViewModel()
        {

        }
        public FilterUsersViewModel(string challengeOwnerId, int point, int range, bool isBotActivated = false)
        {
            ChallengeOwnerId = challengeOwnerId;
            Point = point;
            Range = range;
            IsBotActivated = isBotActivated;
        }
        public string ChallengeOwnerId { get; set; }
        public int Point { get; set; }
        public int Range { get; set; }
        public bool IsBotActivated { get; set; }
    }
}
