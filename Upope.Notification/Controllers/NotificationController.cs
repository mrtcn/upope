using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Upope.Notification.EntityParams;
using Upope.Notification.Hubs;
using Upope.Notification.Services;
using Upope.Notification.Services.Interfaces;
using Upope.Notification.ViewModels;
using Upope.ServiceBase.Extensions;
using Upope.ServiceBase.Models;
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
        private readonly ILoyaltySyncService _loyaltySyncService;
        private readonly IMapper _mapper;

        public NotificationController(
            IHubContext<NotificationHub, ITypedHubClient> hubContext,
            INotificationService notificationService,
            INotificationTemplateService notificationTemplateService,
            IIdentityService identityService,
            ILoyaltySyncService loyaltySyncService,
            IMapper mapper)
        {
            _hubContext = hubContext;
            _notificationService = notificationService;
            _notificationTemplateService = notificationTemplateService;
            _identityService = identityService;
            _loyaltySyncService = loyaltySyncService;
            _mapper = mapper;
        }

        [HttpGet("Notifications/{entityCountToSkip}")]
        [Authorize]
        public async Task<IActionResult> GetNotifications(int entityCountToSkip)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var notifications = _notificationService.GetNotifications(userId, entityCountToSkip);

            var notificationViewModelList = _mapper.Map<NotificationModel>(notifications);

            return Ok(notificationViewModelList);
        }

        [HttpPost("SendNotification")]
        [Authorize]
        public async Task<IActionResult> SendNotification(NotificationModel model)
        {
            var notificationTemplate = _notificationTemplateService.GetNotificationTemplate(model.NotificationType);

            var notificationEntityParams = new NotificationEntityParams()
            {                
                ImagePath = notificationTemplate.ImagePath,
                CreatedDate = DateTime.Now,
                IsActionTaken = false,
                NotificationType = model.NotificationType,
                UserId = model.UserId,
                GameId = model.GameId,
                CreditsToEarn = model.CreditsToEarn,
                UserCredits = model.UserCredits,
                WinStreakCount = model.WinStreakCount
            };

            switch (model.NotificationType)
            {
                case ServiceBase.Enums.NotificationType.WatchAdNotification:
                    notificationEntityParams.Label = notificationTemplate.Label.Replace("{UserCredits}", model.UserCredits.ToString());
                    notificationEntityParams.Description = notificationTemplate.Description.Replace("{WatchingAdCreditReward}", model.CreditsToEarn.ToString());
                    break;
                case ServiceBase.Enums.NotificationType.UpgradeToProNotification:
                    notificationEntityParams.Label = notificationTemplate.Label.Replace("{UserCredits}", model.UserCredits.ToString());
                    notificationEntityParams.Description = notificationTemplate.Description.Replace("{UpgradeToProCreditReward}", model.CreditsToEarn.ToString());
                    break;
                case ServiceBase.Enums.NotificationType.StreakNotification:
                    notificationEntityParams.Label = notificationTemplate.Label.Replace("{CreditsToEarn}", model.CreditsToEarn.ToString());
                    notificationEntityParams.Description = notificationTemplate.Description.Replace("{WinStreakCount}", model.WinStreakCount.ToString());
                    break;
                default:
                    notificationEntityParams.Label = notificationTemplate.Label;
                    notificationEntityParams.Description = notificationTemplate.Description;
                    break;
            }

            _notificationService.CreateOrUpdate(notificationEntityParams);

            var notificationModel = _mapper.Map<NotificationModel>(notificationEntityParams);

            await _hubContext.Clients.User(model.UserId).ReceiveNotification(notificationModel);

            return Ok();
        }


        [HttpPost("ClaimCredits")]
        [Authorize]
        public async Task<IActionResult> ClaimCredits(ClaimCreditsViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);
            
            await _loyaltySyncService.AddCredit(new CreditsViewModel(userId, model.Credits), accessToken);

            if (model.NotificationId == null)
                return Ok();

            var notification = _notificationService.Get(model.NotificationId.GetValueOrDefault());
            var notificationEntityParams = _mapper.Map<NotificationEntityParams>(notification);
            notificationEntityParams.IsActionTaken = true;

            _notificationService.CreateOrUpdate(notificationEntityParams);
            var notificationModel = _mapper.Map<NotificationModel>(notificationEntityParams);

            return Ok(notificationModel);
        }
    }
}