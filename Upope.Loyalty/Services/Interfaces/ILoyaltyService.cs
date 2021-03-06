﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Loyalty.EntityParams;
using Upope.Loyalty.Models;
using Upope.ServiceBase;

namespace Upope.Loyalty.Services.Interfaces
{
    public interface ILoyaltyService : IEntityServiceBase<Data.Entities.Loyalty>
    {
        int? UserCredit(string userId);
        LoyaltyParams GetLoyaltyByUserId(string userId);
        Task<List<LoyaltyParams>> SufficientPoints(string accessToken, int point, bool isBotActivated = false);
        List<string> ExcludeOutOfRangeUsers(int range, string actualUserId, List<string> destinationUserIds);
        void ChargeCredits(ChargeCreditsParams chargeCreditsParams);
        void AddCredits(ChargeCreditsParams chargeCreditsParams);
        void AddScores(AddScoresParams chargeScoresParams);
        void AddWin(string userId);
        void ResetWin(string userId);
        UserStatsModel GetUserStats(string userId);
        List<WinLeadershipBoard> WinLeaderships(int page);
        List<ScoreLeadershipBoard> ScoreLeaderships(int page);
        List<CreditLeadershipBoard> CreditLeaderships(int page);
    }
}
