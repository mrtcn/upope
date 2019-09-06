using System.Threading.Tasks;
using Upope.Game.Models;

namespace Upope.Game.Services.Interfaces
{
    public interface ILoyaltySyncService
    {
        Task ChargeCredit(CreditsModel model, string accessToken);
        Task AddCredit(CreditsModel model, string accessToken);
        Task AddScores(ScoreModel model, string accessToken);
        Task AddWin(string userId, string accessToken);
        Task ResetWin(string userId, string accessToken);
    }
}
