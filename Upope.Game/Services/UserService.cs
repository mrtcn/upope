using AutoMapper;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.Game.GlobalSettings;
using Upope.Game.Services.Interfaces;
using Upope.Game.Services.Models;
using Upope.ServiceBase;
using Upope.ServiceBase.Handler;

namespace Upope.Game.Services
{
    public class UserService : EntityServiceBase<User>, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IHttpHandler _httpHandler;

        public UserService(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper,
            IHttpHandler httpHandler) : base(applicationDbContext, mapper)
        {
            _mapper = mapper;
            _httpHandler = httpHandler;
        }

        public UserParams GetUserByUserId(string userId)
        {
            var user = Entities.FirstOrDefault(x => x.UserId == userId);
            var userParams = _mapper.Map<User, UserParams>(user);

            return userParams;
        }

        public async Task<TokenModel> Login(LoginModel model)
        {
            var baseUrl = AppSettingsProvider.IdentityBaseUrl;

            var api = AppSettingsProvider.Login;

            var messageBody = JsonConvert.SerializeObject(model);
            var result = await _httpHandler.PostAsync<TokenModel>(baseUrl, api, messageBody);

            return result;
        }
    }
}
