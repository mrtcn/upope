using AutoMapper;
using Upope.Identity.GlobalSettings;
using Upope.Identity.Services.Model;
using Upope.ServiceBase.Handler;

namespace Upope.Identity.Services
{
    public interface IProfileService
    {

    }
    public class ProfileService : IProfileService
    {
        private readonly IHttpHandler _httpHandler;
        private readonly IMapper _mapper;

        public ProfileService(IMapper mapper, IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
            _mapper = mapper;
        }

        //public int GetPointByUserId(HttpCallModel model)
        //{
        //    if (string.IsNullOrEmpty(model.BaseUrl))
        //        model.BaseUrl = AppSettingsProvider.LoyaltyBaseUrl;

        //    if (string.IsNullOrEmpty(model.Api))
        //        model.Api = "/api/Loyalty/SufficientPoints";

        //} 
    }
}
