using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using ChatEntity = Upope.Chat.Data.Entities.Chat;
using Upope.Chat.Services.Interfaces;
using Upope.ServiceBase;
using Upope.ServiceBase.Services.Interfaces;
using System.Collections.Generic;
using Upope.Chat.Models;

namespace Upope.Chat.Services
{    
    public class ChatService : EntityServiceBase<ChatEntity>, IChatService
    {
        private readonly IIdentityService _identityService;
        private readonly IChatRoomService _chatRoomService;
        public ChatService(
            ApplicationDbContext applicationDbContext,
            IIdentityService identityService,
            IChatRoomService chatRoomService,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _identityService = identityService;
            _chatRoomService = chatRoomService;
        }

        public async Task<List<ChatModel>> GetChats(string accessToken, string chatUserId)
        {
            var userProfile = await _identityService.GetUserProfileByAccessToken(accessToken);
            var chatUserProfile = await _identityService.GetUserProfileById(accessToken, chatUserId);

            var chatRoom = _chatRoomService.GetChatRoom(accessToken, chatUserId);

            IQueryable<ChatModel> chats = GetWholeChats(chatRoom.Id);
            chats = ApplyUserTypeRestrictions(userProfile, chatUserProfile, chats);

            return chats.ToList();
        }

        public async Task<List<ChatModel>> GetChats(string accessToken, int chatRoomId)
        {
            var chatRoom = _chatRoomService.Get(chatRoomId);

            var userProfile = await _identityService.GetUserProfileById(accessToken, chatRoom.UserId);
            var chatUserProfile = await _identityService.GetUserProfileById(accessToken, chatRoom.ChatUserId);

            IQueryable<ChatModel> chats = GetWholeChats(chatRoomId);
            chats = ApplyUserTypeRestrictions(userProfile, chatUserProfile, chats);

            return chats.ToList();
        }

        private static IQueryable<ChatModel> ApplyUserTypeRestrictions(ServiceBase.Models.UserProfile userProfile, ServiceBase.Models.UserProfile chatUserProfile, IQueryable<ChatModel> chats)
        {
            if (userProfile.UserType != ServiceBase.Enums.UserType.Premium && chatUserProfile.UserType != ServiceBase.Enums.UserType.Premium)
                chats = chats.Take(25);
            return chats;
        }

        private IQueryable<ChatModel> GetWholeChats(int chatRoomId)
        {
            return Entities.Where(x => x.ChatRoomId == chatRoomId && x.Status == ServiceBase.Enums.Status.Active)
                 .OrderByDescending(x => x.Id)
                 .Select(x => new ChatModel()
                 {
                     ChatRoomId = x.ChatRoomId,
                     Text = x.Message,
                     UserId = x.UserId,
                     CreatedDate = x.CreatedDate
                 });
        }
    }
}
