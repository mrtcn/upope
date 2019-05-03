using System.Collections.Generic;
using System.Threading.Tasks;
using Upope.Game.EntityParams;
using Upope.Game.Enum;

namespace Upope.Game.Hubs.Interfaces
{
    public interface IGameHub
    {
        Task AskBluff(string userId);
        Task TextBluff(string userId, RockPaperScissorsType choice);
        Task RoundEnds(List<string> userIds, RoundResult roundResult);
    }
}
