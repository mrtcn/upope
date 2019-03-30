using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Game.Services.Interfaces;

namespace Upope.Challenge.Hubs
{
    [Authorize]
    public class GameHubs : Hub
    {
        private readonly IGameService _gameService;

        public GameHubs(IGameService gameService)
        {
            _gameService = gameService;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();            
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendChallenge(IReadOnlyList<string> userIds, string body)
        {
            await Clients.All.SendAsync("ReceiveMessage", body);
        }
    }
}
