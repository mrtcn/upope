using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Upope.Loyalty.Services.Interfaces;
using Upope.Loyalty.ViewModels;
using Upope.ServiceBase.Extensions;
using Upope.ServiceBase.Handler;

namespace Upope.Loyalty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyController : ControllerBase
    {
        private readonly ILoyaltyService _loyaltyService;
        private readonly IHttpHandler _httpHandler;
        private readonly IMapper _mapper;

        public LoyaltyController(ILoyaltyService loyaltyService, IMapper mapper, IHttpHandler httpHandler)
        {
            _loyaltyService = loyaltyService;
            _httpHandler = httpHandler;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [Route("GetPoint")]
        public IActionResult GetPoint()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            //TODO: GetPointByUserId does not accept accesstoken. Should be fixed
            var point = _loyaltyService.GetPointByUserId(accessToken);

            var pointViewModel = _mapper.Map<GetPointViewModel>(point);

            return Ok(pointViewModel);
        }

        [HttpPost]
        [Authorize]
        [Route("SufficientPoints")]
        public IActionResult SufficientPoints(GetSufficientPointViewModel model)
        {
            var sufficientPoints = _loyaltyService.SufficientPoints(model.Points);
            IReadOnlyList<string> userIds = sufficientPoints.Select(x => x.UserId).ToList<string>();

            return Ok(userIds);
        }
    }
}