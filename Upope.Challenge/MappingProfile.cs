using AutoMapper;
using Upope.Challenge.Data.Entities;
using Upope.Challenge.EntityParams;
using Upope.Challenge.ViewModels;
using ChallengeEntity = Upope.Challenge.Data.Entities.Challenge;

namespace Upope.Challenge
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ChallengeEntity, ChallengeParams>();
            CreateMap<ChallengeParams, ChallengeEntity>();

            CreateMap<ChallengeRequest, ChallengeRequestParams>();
            CreateMap<ChallengeRequestParams, ChallengeRequest>();

            CreateMap<UserParams, User>();
            CreateMap<User, UserParams>();

            CreateMap<CreateUserViewModel, UserParams>();
            CreateMap<UserParams, CreateUserViewModel>();
        }
    }
}
