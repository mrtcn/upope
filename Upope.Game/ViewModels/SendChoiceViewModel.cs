﻿using Upope.Game.Enum;

namespace Upope.Game.ViewModels
{
    public class SendChoiceViewModel
    {
        public int GameId { get; set; }
        public string UserId { get; set; }
        public RockPaperScissorsType Choice { get; set; }
    }
}