using AutoMapper;
using Upope.Challange.Data.Entities;
using Upope.Challange.EntityParams;

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
        }
    }
}
