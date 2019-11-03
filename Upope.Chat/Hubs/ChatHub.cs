using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
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
        private readonly IChatRoomService _chatRoomService;
        private readonly IIdentityService _identityService;
        private readonly IStringLocalizer<ChatHub> _localizer;

        public ChatHub(
            IChatService chatService,
            IChatRoomService chatRoomService,
            IIdentityService identityService,
            IStringLocalizer<ChatHub> localizer)
        {
            _chatService = chatService;
            _chatRoomService = chatRoomService;
            _identityService = identityService;
            _localizer = localizer;
        }

        public async Task SendMessage(int chatRoomId, string chatUserId, string message)
        {
            var accessToken = this.Context.GetHttpContext().Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var chatRoom = _chatRoomService.GetChatRoom(userId, chatUserId);
            if(chatRoom == null)
                await Clients.User(userId).SendAsync("ErrorOnMessage", _localizer.GetString("NotContact"));

            var chatParams = new ChatParams(accessToken, chatRoomId, chatUserId, message);
            _chatService.CreateOrUpdate(chatParams);
            await Clients.User(chatUserId).SendAsync("ReceiveMessage", message);
        }


        //[HttpPost("Contact/{contactUserId}")]
        //public async Task<IActionResult> CreateContact(string contactUserId)
        //{
        //    var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
        //    var userId = await _identityService.GetUserId(accessToken);

        //    if (!_contactService.IsInContact(userId, contactUserId))
        //        _contactService.CreateOrUpdate(new ContactParams(userId, contactUserId));

        //    return Ok(true);
        //}

        //[HttpDelete("Contact/{contactUserId}")]
        //public async Task<IActionResult> RemoveContact(string contactUserId)
        //{
        //    var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
        //    var userId = await _identityService.GetUserId(accessToken);

        //    var contactParams = _contactService.GetContact(userId, contactUserId);
        //    _contactService.Remove(new RemoveEntityParams(contactParams.Id, null, true, false));

        //    return Ok(true);
        //}

        //[HttpPost("ChatRoom/{chatUserId}")]
        //public async Task<IActionResult> CreateChatRoom(string chatUserId)
        //{
        //    var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
        //    var userId = await _identityService.GetUserId(accessToken);

        //    var isInContact = _contactService.IsInContact(userId, chatUserId);

        //    if (!isInContact)
        //        return BadRequest(_localizer.GetString("NotContact"));

        //    var chatRoom = _chatRoomService.GetChatRoom(userId, chatUserId);
        //    if (chatRoom != null)
        //        return Ok(new { ChatRoomId = chatRoom.Id });

        //    var chatRoomParams = _chatRoomService.CreateOrUpdate(new ChatRoomParams(accessToken, chatUserId));

        //    return Ok(new { ChatRoomId = chatRoomParams.Id });
        //}

        //[HttpGet("Chats/{chatRoomId}")]
        //public async Task<IActionResult> GetChats(int chatRoomId)
        //{
        //    var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
        //    var chats = await _chatService.GetChats(accessToken, chatRoomId);

        //    return Ok(chats);
        //}

        //[HttpGet("ChatRooms")]
        //public async Task<IActionResult> ChatRooms()
        //{
        //    var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
        //    var chatRooms = await _chatRoomService.ChatRooms(accessToken);

        //    return Ok(chatRooms);
        //}
    }
}
