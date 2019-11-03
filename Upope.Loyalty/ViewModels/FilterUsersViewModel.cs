using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upope.Loyalty.ViewModels
{
    public class FilterUsersViewModel
    {
        public FilterUsersViewModel()
        {

        }
        public FilterUsersViewModel(string challengeOwnerId, int point, int range, bool isBotActivated = false)
        {

        }
        public string ChallengeOwnerId { get; set; }
        public int Point { get; set; }
        public int Range { get; set; }
        public bool IsBotActivated { get; set; }
    }
}
