using System.Threading.Tasks;
using Upope.Chat.Data.Entities;
using Upope.Chat.EntityParams;
using Upope.ServiceBase;

namespace Upope.Chat.Services.Interfaces
{
    public interface IChatService : IEntityServiceBase<ChatRoom>
    {
        Task<ChatRoomParams> GetChatRoom(string accessToken, string chatUserId);
        ChatRoomParams GetChatRoomById(int Id);
    }
}
