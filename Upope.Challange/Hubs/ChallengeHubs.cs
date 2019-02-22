using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Challange.EntityParams;
using Upope.Challange.Services.Interfaces;
using Upope.Challange.ViewModels;

namespace Upope.Challange.Hubs
{
    public class ChallengeHubs : Hub
    {
        private readonly IChallengeService _challengeService;

        public ChallengeHubs(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        public override Task OnConnectedAsync()
        {
            var id = this.Context.User.Identity.Name;

            return base.OnConnectedAsync();            
        }

        [Authorize]
        public async Task SendMessage(string user, string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", user, message);
        }

        [Authorize]
        public async Task SendChallenge(IReadOnlyList<string> userIds, string body)
        {
            await Clients.All.SendAsync("ReceiveMessage", body);
        }

        [Authorize]
        public async Task CreateChallenge(CreateChallengeViewModel model)
        {
            //var accessToken = HttpContext.Token;
            //var userId = await _challengeService.GetUserId(accessToken);

            //var challengeParams = new ChallengeParams(ServiceBase.Enums.Status.Active, userId);
            //var challenge = _challengeService.CreateOrUpdate(challengeParams);

            //await Clients.Users(userIds).SendAsync("ReceiveMessage", body);

        }
    }
}
