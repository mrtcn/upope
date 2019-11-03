using AutoMapper;
using Upope.Identity.ViewModels;
using Upope.Loyalty.EntityParams;
using LoyaltyEntity = Upope.Loyalty.Data.Entities.Loyalty;
using Upope.Loyalty.ViewModels;
using Upope.Loyalty.Data.Entities;
using Upope.Loyalty.Models;

namespace Upope.Loyalty
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PointViewModel, AddScoresParams>();
            CreateMap<AddScoresParams, PointViewModel>();

            CreateMap<LoyaltyEntity, LoyaltyParams>();
            CreateMap<LoyaltyParams, LoyaltyEntity>();

            CreateMap<LoyaltyParams, GetPointViewModel>();
            CreateMap<GetPointViewModel, LoyaltyParams>();

            CreateMap<LoyaltyParams, PointViewModel>();
            CreateMap<PointViewModel, LoyaltyParams>();

            CreateMap<LoyaltyParams, CreateOrUpdateViewModel>();
            CreateMap<CreateOrUpdateViewModel, LoyaltyParams>();

            CreateMap<CreditsViewModel, ChargeCreditsParams>();
            CreateMap<ChargeCreditsParams, CreditsViewModel>();

            CreateMap<User, UserParams>();
            CreateMap<UserParams, User>();

            CreateMap<UserParams, CreateUserViewModel>();
            CreateMap<CreateUserViewModel, UserParams>();

            CreateMap<WinLeadershipBoard, WinLeadershipBoardViewModel>();
            CreateMap<ScoreLeadershipBoard, ScoreLeadershipBoardViewModel>();
            CreateMap<CreditLeadershipBoard, CreditLeadershipBoardViewModel>();

            CreateMap<LeadershipBoardBase, LeadershipBoardBaseViewModel>()
                .Include<WinLeadershipBoard, WinLeadershipBoardViewModel>()
                .Include<ScoreLeadershipBoard, ScoreLeadershipBoardViewModel>()
                .Include<CreditLeadershipBoard, CreditLeadershipBoardViewModel>();

            CreateMap<WinLeadershipBoardViewModel, WinLeadershipBoard>();
            CreateMap<ScoreLeadershipBoardViewModel, ScoreLeadershipBoard>();
            CreateMap<CreditLeadershipBoardViewModel, CreditLeadershipBoard>();

            CreateMap<LeadershipBoardBaseViewModel, LeadershipBoardBase>()
                .Include<WinLeadershipBoardViewModel, WinLeadershipBoard>()
                .Include<ScoreLeadershipBoardViewModel, ScoreLeadershipBoard>()
                .Include<CreditLeadershipBoardViewModel, CreditLeadershipBoard>();
        }
    }
}
