﻿using System;
using Upope.Loyalty.Data.Entities;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.Models;

namespace Upope.Loyalty.EntityParams
{
    public class LoyaltyParams: IEntityParams, ILoyalty, IOperatorFields
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public int CurrentWinStreak { get; set; }
        public int WinRecord { get; set; }
        public int Credit { get; set; }
        public int Score { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
