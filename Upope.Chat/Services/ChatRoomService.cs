using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upope.Chat.Data.Entities;
using Upope.Chat.EntityParams;
using Upope.Chat.Services.Interfaces;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Models;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.Chat.Services
{    
    public class ChatRoomService : EntityServiceBase<ChatRoom>, IChatRoomService
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public ChatRoomService(
            ApplicationDbContext applicationDbContext,
            IIdentityService identityService,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _mapper = mapper;
            _identityService = identityService;
        }

        protected override async void OnSaveChanges(IEntityParams entityParams, ChatRoom entity)
        {
            
            var chatRoomParams = entityParams as ChatRoomParams;
            var userId = await _identityService.GetUserId(chatRoomParams.AccessToken);

            var chatRoomExists = Entities
                .Any(x =>
                ((x.UserId == entity.UserId && x.ChatUserId == userId)
                || (x.UserId == userId && x.ChatUserId == entity.UserId))
                && x.Status == Status.Active);

            if (chatRoomExists)
                return;

            chatRoomParams.UserId = userId;
            entity.UserId = userId;

            base.OnSaveChanges(chatRoomParams, entity);
        }

        public ChatRoomParams GetChatRoom(string userId, string chatUserId)
        {
            var chatRoom = Entities.FirstOrDefault(x => x.UserId == userId && x.ChatUserId == chatUserId && x.Status == Status.Active);

            var chatRoomParams = _mapper.Map<ChatRoomParams>(chatRoom);
            return chatRoomParams;
        }

        public ChatRoomParams GetChatRoomById(int Id)
        {
            var chatRoom = Entities.FirstOrDefault(x => x.Id == Id && x.Status == Status.Active);
            var chatRoomParams = _mapper.Map<ChatRoomParams>(chatRoom);
            return chatRoomParams;
        }

        public async Task<List<ChatRoomParams>> ChatRooms(string accessToken)
        {
            var userId = await _identityService.GetUserId(accessToken);
            var chatRooms = Entities
                .Where(x => (x.UserId == userId || x.ChatUserId == userId) && x.Status == Status.Active)
                .Select(x => new ChatRoomParams() {
                    Id = x.Id,
                    ChatUserId = x.ChatUserId,
                    UserId = x.UserId,
                    CreatedDate = x.CreatedDate
                })
                .ToList();

            return chatRooms;
        }
    }
}
