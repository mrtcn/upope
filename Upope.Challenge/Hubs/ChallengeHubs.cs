using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Challenge.EntityParams;
using Upope.Challenge.Services.Interfaces;
using Upope.Challenge.ViewModels;

namespace Upope.Challenge.Hubs
{
    [Authorize]
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

        public async Task SendMessage(string user, string message)
        {
            var xx = Context.User;
            await Clients.Caller.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendChallenge(IReadOnlyList<string> userIds, string body)
        {
            await Clients.All.SendAsync("ReceiveMessage", body);
        }

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
