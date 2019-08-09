using System.Threading.Tasks;
using ChatEntity = Upope.Chat.Data.Entities.Chat;
using Upope.ServiceBase;
using Upope.Chat.Models;
using System.Collections.Generic;

namespace Upope.Chat.Services.Interfaces
{
    public interface IChatService : IEntityServiceBase<ChatEntity>
    {
        Task<List<ChatModel>> GetChats(string accessToken, string chatUserId);
        Task<List<ChatModel>> GetChats(string accessToken, int chatRoomId);
    }
}
