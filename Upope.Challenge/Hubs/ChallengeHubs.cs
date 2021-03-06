﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Challenge.Services.Interfaces;
using Upope.Challenge.ViewModels;

namespace Upope.Challenge.Hubs
{
    [Authorize]
    public class ChallengeHub : Hub
    {
        private readonly IChallengeService _challengeService;

        public ChallengeHub(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();            
        }

        public async Task SendMessage(UserMessage model )
        {
            await Clients.Caller.SendAsync("ReceiveMessage", model.user, model.message);
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

    public class UserMessage
    {
        public string user { get; set; }
        public string message { get; set; }
    }
}
