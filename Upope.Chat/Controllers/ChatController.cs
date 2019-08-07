using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Upope.Chat.EntityParams;
using Upope.Chat.Services.Interfaces;
using Upope.ServiceBase.Extensions;

namespace Upope.Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("ChatRoom/{chatUserId}")]
        public IActionResult CreateChatRoom(string chatUserId)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var chatRoom = _chatService.CreateOrUpdate(new ChatRoomParams(accessToken, chatUserId));

            return Ok(new { ChatRoomId = chatRoom.Id });
        }
    }
}