using AutoMapper;
using Upope.Challange.Data.Entities;
using Upope.Challange.EntityParams;
using Upope.Challange.ViewModels;

namespace Upope.Challange
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Challenge, ChallengeParams>();
            CreateMap<ChallengeParams, Challenge>();

            CreateMap<ChallengeRequest, ChallengeRequestParams>();
            CreateMap<ChallengeRequestParams, ChallengeRequest>();

            CreateMap<UserParams, User>();
            CreateMap<User, UserParams>();

            CreateMap<CreateUserViewModel, UserParams>();
            CreateMap<UserParams, CreateUserViewModel>();
        }
    }
}
