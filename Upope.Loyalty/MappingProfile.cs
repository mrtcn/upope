using AutoMapper;
using Upope.Identity.ViewModels;
using Upope.Loyalty.EntityParams;
using Upope.Loyalty.ViewModels;

namespace Upope.Challenge
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Loyalty.Data.Entities.Loyalty, LoyaltyParams>();
            CreateMap<LoyaltyParams, Loyalty.Data.Entities.Loyalty>();

            CreateMap<LoyaltyParams, GetPointViewModel>();
            CreateMap<GetPointViewModel, LoyaltyParams>();

            CreateMap<LoyaltyParams, PointViewModel>();
            CreateMap<PointViewModel, LoyaltyParams>();

            CreateMap<LoyaltyParams, CreateOrUpdateViewModel>();
            CreateMap<CreateOrUpdateViewModel, LoyaltyParams>();
        }
    }
}
