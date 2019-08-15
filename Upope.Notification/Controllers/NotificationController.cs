using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Upope.Notification.EntityParams;
using Upope.Notification.Hubs;
using Upope.Notification.Models;
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
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public NotificationController(
            IHubContext<NotificationHub, ITypedHubClient> hubContext,
            INotificationService notificationService,
            INotificationTemplateService notificationTemplateService,
            IIdentityService identityService,
            IMapper mapper)
        {
            _hubContext = hubContext;
            _notificationService = notificationService;
            _notificationTemplateService = notificationTemplateService;
            _identityService = identityService;
            _mapper = mapper;
        }

        [HttpGet("Notifications/{entityCountToSkip}")]
        [Authorize]
        public async Task<IActionResult> GetNotifications(int entityCountToSkip)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var notifications = _notificationService.GetNotifications(userId, entityCountToSkip);

            var notificationViewModelList = _mapper.Map<NotificationViewModel>(notifications);

            return Ok(notificationViewModelList);
        }

        [HttpPost("SendNotification")]
        [Authorize]
        public async Task<IActionResult> SendNotification(NotificationViewModel model)
        {
            var notificationTemplate = _notificationTemplateService.GetNotificationTemplate(model.NotificationType);
            var label = notificationTemplate.Label;
            var description = notificationTemplate.Description;
            var imagePath = notificationTemplate.ImagePath;

            var notificationEntityParams = _notificationService.CreateOrUpdate(new NotificationEntityParams()
            {
                Label = label,
                Description = description,
                ImagePath = imagePath,
                CreatedDate = DateTime.Now,
                IsActionTaken = false,
                NotificationType = model.NotificationType,
                UserId = model.UserId,
                GameId = model.GameId
            });

            var notificationModel = _mapper.Map<NotificationModel>(notificationEntityParams);

            await _hubContext.Clients.User(model.UserId).BroadcastMessage(notificationModel);

            return Ok();
        }
    }
}