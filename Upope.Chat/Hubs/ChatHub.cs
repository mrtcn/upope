using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Upope.Chat.EntityParams;
using Upope.Chat.Services.Interfaces;
using Upope.ServiceBase.Extensions;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.Chat.Hubs
{
    public class ChatHub: Hub
    {
        private readonly IChatService _chatService;
        private readonly IIdentityService _identityService;
        public ChatHub(
            IChatService chatService,
            IIdentityService identityService)
        {
            _chatService = chatService;
            _identityService = identityService;
        }

        public async Task SendMessage(int chatRoomId, string chatUserId, string message)
        {
            var accessToken = this.Context.GetHttpContext().Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var chatParams = new ChatParams(accessToken, chatRoomId, chatUserId, message);
            _chatService.CreateOrUpdate(chatParams);
            await Clients.User(chatUserId).SendAsync("ReceiveMessage", message);
        }
    }
}
