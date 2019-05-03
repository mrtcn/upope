using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Game.EntityParams;
using Upope.Game.Enum;
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

        public async Task AskBluff(string userId)
        {
            await Clients.User(userId).SendAsync("AskBluff");
        }

        public async Task TextBluff(string userId, RockPaperScissorsType choice)
        {
            await Clients.User(userId).SendAsync("TextBluff", choice);
        }

        public async Task RoundEnds(List<string> userIds, RoundResult roundResult)
        {
            await Clients.Users(userIds).SendAsync("RoundEnds", roundResult);
        }
        
    }
}
