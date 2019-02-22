using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upope.Loyalty.Data.Entities;
using Upope.Loyalty.EntityParams;
using Upope.Loyalty.GlobalSettings;
using Upope.Loyalty.Services.Interfaces;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Handler;

namespace Upope.Loyalty.Services
{    
    public class LoyaltyService : EntityServiceBase<Point>, ILoyaltyService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpHandler _httpHandler;
        private readonly IMapper _mapper;

        public LoyaltyService(ApplicationDbContext applicationDbContext, IMapper mapper, IHttpHandler httpHandler) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _httpHandler = httpHandler;
            _mapper = mapper;
        }

        public PointParams GetPointByUserId(string userId)
        {
            var point = Entities.FirstOrDefault(x => x.UserId == userId && x.Status == Status.Active);
            var pointParams = _mapper.Map<PointParams>(point);

            return pointParams;
        }

        public List<PointParams> SufficientPoints(int point)
        {
            var sufficientPoints = Entities.Where(x => x.Points >= point && x.Status == Status.Active).Take(5);
            var pointParams = _mapper.Map<List<PointParams>>(sufficientPoints);

            return pointParams;
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
