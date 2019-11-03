using AutoMapper;
using Upope.Identity.Data.Entities;
using Upope.Identity.Entities;
using Upope.Identity.EntityParams;
using Upope.Identity.Models;
using Upope.Identity.ViewModels;

namespace Upope.Identity
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProfileViewModel, ApplicationUser>();
            CreateMap<ApplicationUser, ProfileViewModel>();

            CreateMap<ApplicationUser, CreateOrUpdateLoyaltyUserViewModel>();
            CreateMap<CreateOrUpdateLoyaltyUserViewModel, ApplicationUser>();          

            CreateMap<ApplicationUser, CreateOrUpdateChallengeUserViewModel>();
            CreateMap<CreateOrUpdateChallengeUserViewModel, ApplicationUser>();

            CreateMap<ApplicationUser, CreateOrUpdateGameUserViewModel>();
            CreateMap<CreateOrUpdateGameUserViewModel, ApplicationUser>();

            CreateMap<ImageParams, Image>();
            CreateMap<Image, ImageParams>();
            
            CreateMap<ImageListModel, ImageParams>();
            CreateMap<ImageParams, ImageListModel>();

            CreateMap<ImageListModel, Image>();
            CreateMap<Image, ImageListModel>();

            CreateMap<AddImageByUrlViewModel, ImageParams>();
            CreateMap<ImageParams, AddImageByUrlViewModel>();
        }
    }
}
