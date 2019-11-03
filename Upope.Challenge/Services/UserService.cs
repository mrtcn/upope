using AutoMapper;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Upope.Challenge.Data.Entities;
using Upope.Challenge.EntityParams;
using Upope.Challenge.GlobalSettings;
using Upope.Challenge.Services.Interfaces;
using Upope.Challenge.Services.Models;
using Upope.ServiceBase;
using Upope.ServiceBase.Handler;

namespace Upope.Challenge.Services
{
    public class UserService : EntityServiceBase<User>, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IHttpHandler _httpHandler;
        public UserService(
            ApplicationDbContext applicationDbContext,
            IHttpHandler httpHandler,
            IMapper mapper) : base(applicationDbContext, mapper)
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
