using AutoMapper;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.Game.ViewModels;
using GameEntity = Upope.Game.Data.Entities.Game;
namespace Upope.Game
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameEntity, GameParams>();
            CreateMap<GameParams, GameEntity>();
            CreateMap<CreateOrUpdateViewModel, GameParams>();
            CreateMap<GameParams, CreateOrUpdateViewModel>();
            CreateMap<SendChoiceViewModel, SendChoiceParams>();
            CreateMap<SendBluffViewModel, BluffParams>();
            CreateMap<GameRound, GameRoundParams>();
            CreateMap<GameRoundParams, GameRound>();
            CreateMap<Bluff, BluffParams>();
            CreateMap<BluffParams, Bluff>();
            CreateMap<CreateUserViewModel, UserParams>();
            CreateMap<UserParams, CreateUserViewModel>();
            CreateMap<User, UserParams>();
            CreateMap<UserParams, User>();
        }
    }
}
