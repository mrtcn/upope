﻿
namespace Upope.Game.Models
{
    public class CreditsModel
    {
        public CreditsModel(string userId, int credit)
        {
            UserId = userId;
            Credit = credit;
        }
        public int Credit { get; set; }
        public string UserId { get; set; }        
    }
}
