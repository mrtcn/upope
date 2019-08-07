using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Upope.Chat.Data.Entities;
using Upope.Chat.EntityParams;
using Upope.Chat.Services.Interfaces;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Models;

namespace Upope.Chat.Services
{    
    public class ChatService : EntityServiceBase<ChatRoom>, IChatService
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public ChatService(
            ApplicationDbContext applicationDbContext,
            IIdentityService identityService,
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _mapper = mapper;
            _identityService = identityService;
        }

        protected override async void OnSaveChanges(IEntityParams entityParams, ChatRoom entity)
        {
            var chatParams = entityParams as ChatRoomParams;
            var userId = await _identityService.GetUserId(chatParams.AccessToken);
            chatParams.UserId = userId;

            base.OnSaveChanges(chatParams, entity);
        }

        public async Task<ChatRoomParams> GetChatRoom(string accessToken, string chatUserId)
        {
            var userId = await _identityService.GetUserId(accessToken);

            var chat = Entities.FirstOrDefault(x => x.UserId == userId && x.ChatUserId == chatUserId && x.Status == Status.Active);

            var chatParams = _mapper.Map<ChatRoomParams>(chat);
            return chatParams;
        }

        public ChatRoomParams GetChatRoomById(int Id)
        {
            var chat = Entities.FirstOrDefault(x => x.Id == Id && x.Status == Status.Active);
            var chatParams = _mapper.Map<ChatRoomParams>(chat);
            return chatParams;
        }
    }
}
