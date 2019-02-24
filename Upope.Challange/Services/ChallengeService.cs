using AutoMapper;
using System.Threading.Tasks;
using Upope.Challange.Data.Entities;
using Upope.Challange.GlobalSettings;
using Upope.Challange.Services.Interfaces;
using Upope.ServiceBase;
using Upope.ServiceBase.Handler;

namespace Upope.Challange.Services
{
    public class ChallengeService: EntityServiceBase<Challenge>, IChallengeService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpHandler _httpHandler;
        private readonly IMapper _mapper;

        public ChallengeService(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper, 
            IHttpHandler httpHandler) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _httpHandler = httpHandler;
            _mapper = mapper;
        }

        public async Task<string> GetUserId(string token, string baseUrl = null, string api = null)
        {
            if (string.IsNullOrEmpty(baseUrl))
                baseUrl = AppSettingsProvider.IdentityBaseUrl;

            if (string.IsNullOrEmpty(api))
                api = "/api/Account/GetUserId";

            var userId = await _httpHandler.AuthPostAsync<string>(token, baseUrl, api);

            return userId;
        }
    }
}
