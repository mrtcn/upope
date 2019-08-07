using AutoMapper;
using Upope.Chat.EntityParams;
using ChatEntity = Upope.Chat.Data.Entities.ChatRoom;

namespace Upope.Chat
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ChatEntity, ChatRoomParams>();
            CreateMap<ChatRoomParams, ChatEntity>();
        }
    }
}
