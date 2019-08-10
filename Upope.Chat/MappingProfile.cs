using AutoMapper;
using Upope.Chat.Data.Entities;
using Upope.Chat.EntityParams;
using ChatRoomEntity = Upope.Chat.Data.Entities.ChatRoom;
using ChatEntity = Upope.Chat.Data.Entities.Chat;

namespace Upope.Chat
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ChatRoomEntity, ChatRoomParams>();
            CreateMap<ChatRoomParams, ChatRoomEntity>();

            CreateMap<Contact, ContactParams>();
            CreateMap<ContactParams, Contact>();

            CreateMap<ChatEntity, ChatParams>();
            CreateMap<ChatParams, ChatEntity>();
        }
    }
}
