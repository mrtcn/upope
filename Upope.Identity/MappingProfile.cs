using AutoMapper;
using Upope.Identity.Entities;
using Upope.Identity.ViewModels;

namespace Upope.Identity
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProfileViewModel, ApplicationUser>();
            CreateMap<ApplicationUser, ProfileViewModel>();

            CreateMap<ApplicationUser, CreateOrUpdateChallengeUserViewModel>();
            CreateMap<CreateOrUpdateChallengeUserViewModel, ApplicationUser>();
        }
    }
}
