using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Chat.Data.Entities;
using Upope.Chat.EntityParams;
using Upope.ServiceBase;

namespace Upope.Chat.Services.Interfaces
{
    public interface IChatRoomService : IEntityServiceBase<ChatRoom>
    {
        Task<List<ChatRoomParams>> ChatRooms(string accessToken);
        ChatRoomParams GetChatRoom(string userId, string chatUserId);
        ChatRoomParams GetChatRoomById(int Id);
    }
}
