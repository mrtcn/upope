using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Upope.Chat.EntityParams;
using Upope.Chat.Services.Interfaces;
using Upope.ServiceBase.Extensions;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IChatService _chatService;
        private readonly IContactService _contactService;
        private readonly IIdentityService _identityService;
        private readonly IStringLocalizer<ChatController> _localizer;

        public ChatController(
            IChatRoomService chatRoomService,
            IChatService chatService,
            IContactService contactService,
            IIdentityService identityService,
            IStringLocalizer<ChatController> localizer)
        {
            _chatRoomService = chatRoomService;
            _chatService = chatService;
            _contactService = contactService;
            _identityService = identityService;
            _localizer = localizer;
        }

        [HttpPost("ChatRoom/{chatUserId}")]
        public async Task<IActionResult> CreateChatRoom(string chatUserId)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var isInContact = _contactService.IsInContact(userId, chatUserId);

            if (!isInContact)
                return BadRequest(_localizer.GetString("NotContact").Value);

            var chatRoom = _chatRoomService.GetChatRoom(userId, chatUserId);
            if(chatRoom != null)
                return Ok(new { ChatRoomId = chatRoom.Id });

            var chatRoomParams = _chatRoomService.CreateOrUpdate(new ChatRoomParams(accessToken, chatUserId));

            return Ok(new { ChatRoomId = chatRoomParams.Id });
        }

        [HttpGet("Chats/{chatRoomId}")]
        public async Task<IActionResult> GetChats(int chatRoomId)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var chats = await _chatService.GetChats(accessToken, chatRoomId);

            return Ok(chats);
        }

        [HttpGet("ChatRooms")]
        public async Task<IActionResult> ChatRooms()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var chatRooms = await _chatRoomService.ChatRooms(accessToken);

            return Ok(chatRooms);
        }
    }
}