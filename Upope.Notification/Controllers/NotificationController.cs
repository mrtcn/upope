using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Upope.Notification.Hubs;
using Upope.Notification.Services;
using Upope.Notification.ViewModels;
using Upope.ServiceBase.Extensions;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.Notification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub, ITypedHubClient> _hubContext;
        private readonly INotificationService _notificationService;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public NotificationController(
            IHubContext<NotificationHub, ITypedHubClient> hubContext,
            INotificationService notificationService,
            IIdentityService identityService,
            IMapper mapper)
        {
            _hubContext = hubContext;
            _notificationService = notificationService;
            _identityService = identityService;
            _mapper = mapper;
        }

        [HttpGet("Notifications")]
        [Authorize]
        public async Task<IActionResult> GetNotifications()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var notifications = _notificationService.GetNotifications(userId);

            var notificationViewModelList = _mapper.Map<NotificationViewModel>(notifications);

            return Ok(notificationViewModelList);
        }

        [HttpPost("SendNotification")]
        [Authorize]
        public async Task<IActionResult> SendNotification(NotificationViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            await _hubContext.Clients.User(userId).BroadcastMessage(userId, model);

            return Ok();
        }
    }
}