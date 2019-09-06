using System.Threading.Tasks;
using Upope.Game.EntityParams;
using Upope.Game.Models;
using Upope.Game.ViewModels;

namespace Upope.Game.Interfaces
{
    public interface IGameManager
    {
        void RejectRematch(string userId, string requestingUserId);
        void SendRematch(string userId, string requestingUserId, int credit, int maxCredit);
        void SendRematchRaise(string userId, string requestingUserId, int credit, int maxCredit);
        GameRoundParams CreateOrUpdateGame(CreateOrUpdateViewModel model);
        Task<RoundEndModel> SendChoice(SendChoiceViewModel model, string userId, string accessToken);
        Task<bool> SendBluff(string userId, SendBluffViewModel model);
    }
}
