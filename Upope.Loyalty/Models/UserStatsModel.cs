﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upope.Loyalty.Models
{
    public class UserStatsModel
    {
        public int CurrentWinStreak { get; set; }
        public int WinRecord { get; set; }
        public int Credit { get; set; }
        public int Score { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string ImagePath { get; set; }
    }
}
