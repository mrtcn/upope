using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upope.Loyalty.EntityParams;
using Upope.Loyalty.Models;
using Upope.Loyalty.Services.Interfaces;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.Loyalty.Services
{    
    public class LoyaltyService : EntityServiceBase<Data.Entities.Loyalty>, ILoyaltyService
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly IUserService _userService;

        public LoyaltyService(
            ApplicationDbContext applicationDbContext,
            IIdentityService identityService,
            IUserService userService,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _mapper = mapper;
            _identityService = identityService;
            _userService = userService;
        }

        public int? UserCredit(string userId)
        {
            var loyalty = Entities.FirstOrDefault(x => x.UserId == userId && x.Status == Status.Active);

            if (loyalty == null)
                return null;

            return loyalty.Credit;
        }

        public LoyaltyParams GetLoyaltyByUserId(string userId)
        {
            var loyalty = Entities.FirstOrDefault(x => x.UserId == userId && x.Status == Status.Active);
            var loyaltyParams = _mapper.Map<LoyaltyParams>(loyalty);

            return loyaltyParams;
        }

        public async Task<List<LoyaltyParams>> SufficientPoints(string accessToken, int point)
        {
            var userId = await _identityService.GetUserId(accessToken);

            var sufficientPoints = Entities
                .Where(x => x.Credit >= point 
                    && x.Status == Status.Active
                    && x.UserId != userId)
                .Take(5).ToList();

            var loyaltyParams = _mapper.Map<List<LoyaltyParams>>(sufficientPoints);

            return loyaltyParams;
        }

        public void ChargeCredits(ChargeCreditsParams chargeCreditsParams)
        {
            var loyalty = GetLoyaltyByUserId(chargeCreditsParams.UserId);
            var loyaltyParams = _mapper.Map<LoyaltyParams>(loyalty);
            loyaltyParams.Credit -= chargeCreditsParams.Credit;

            CreateOrUpdate(loyaltyParams);
        }

        public void AddCredits(ChargeCreditsParams chargeCreditsParams)
        {
            var loyalty = GetLoyaltyByUserId(chargeCreditsParams.UserId);
            var loyaltyParams = _mapper.Map<LoyaltyParams>(loyalty);
            loyaltyParams.Credit += chargeCreditsParams.Credit;

            CreateOrUpdate(loyaltyParams);
        }

        public void AddScores(AddScoresParams chargeScoresParams)
        {
            var loyalty = GetLoyaltyByUserId(chargeScoresParams.UserId);
            var loyaltyParams = _mapper.Map<LoyaltyParams>(loyalty);
            loyaltyParams.Score += chargeScoresParams.Scores;

            CreateOrUpdate(loyaltyParams);
        }

        public void AddWin(string userId)
        {
            var loyalty = GetLoyaltyByUserId(userId);
            var loyaltyParams = _mapper.Map<LoyaltyParams>(loyalty);
            loyaltyParams.CurrentWinStreak += 1;

            if (loyaltyParams.CurrentWinStreak > loyaltyParams.WinRecord)
                loyaltyParams.WinRecord = loyaltyParams.CurrentWinStreak;

            CreateOrUpdate(loyaltyParams);
        }

        public void ResetWin(string userId)
        {
            var loyalty = GetLoyaltyByUserId(userId);
            var loyaltyParams = _mapper.Map<LoyaltyParams>(loyalty);
            loyaltyParams.CurrentWinStreak = 0;

            CreateOrUpdate(loyaltyParams);
        }

        public UserStatsModel GetUserStats(string userId)
        {
            var loyalty = GetLoyaltyByUserId(userId);
            var userInfo = _userService.GetUserByUserId(userId);

            return new UserStatsModel()
            {
                Credit = loyalty.Credit,
                CurrentWinStreak = loyalty.CurrentWinStreak,
                Score = loyalty.Score,
                WinRecord = loyalty.WinRecord,
                ImagePath = userInfo.PictureUrl,
                Username = userInfo.Nickname,
                UserId = userInfo.UserId
            };
        }

        public List<WinLeadershipBoard> WinLeaderships(int page)
        {
            var recordToSkip = (page - 1) * 20;
            var recordToTake = 20;
            var leadershipBoard= Entities
                .Include(x => x.User)
                .Where(x => x.Status == Status.Active && x.WinRecord > 0)
                .OrderByDescending(x => x.WinRecord)
                .Skip(recordToSkip).Take(recordToTake)
                .Select(x => new WinLeadershipBoard(x.WinRecord, x.UserId, x.User.Nickname, x.User.PictureUrl)).ToList();

            return leadershipBoard;
        }

        public List<ScoreLeadershipBoard> ScoreLeaderships(int page)
        {
            var recordToSkip = (page - 1) * 20;
            var recordToTake = 20;
            var leadershipBoard = Entities
                .Include(x => x.User)
                .Where(x => x.Status == Status.Active && x.Score > 0)
                .OrderByDescending(x => x.Score)
                .Skip(recordToSkip).Take(recordToTake)
                .Select(x => new ScoreLeadershipBoard(x.Score, x.UserId, x.User.Nickname, x.User.PictureUrl)).ToList();

            return leadershipBoard;
        }

        public List<CreditLeadershipBoard> CreditLeaderships(int page)
        {
            var recordToSkip = (page - 1) * 20;
            var recordToTake = 20;
            var leadershipBoard = Entities
                .Include(x => x.User)
                .Where(x => x.Status == Status.Active && x.Credit > 0)
                .OrderByDescending(x => x.Credit)
                .Skip(recordToSkip).Take(recordToTake)
                .Select(x => new CreditLeadershipBoard(x.Credit, x.UserId, x.User.Nickname, x.User.PictureUrl)).ToList();

            return leadershipBoard;
        }
    }
}
