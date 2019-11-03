using System.Threading.Tasks;
using Upope.Challenge.ViewModels;

namespace Upope.Challenge.Services.Interfaces
{
    public interface IBotService
    {
        Task AcceptChallengeRequest();
        Task SendAnswer();
        Task SendBluff();
        Task SendSuperBluff();
        Task SendUpdateChallengeRequest(string accessToken, int challengeRequestId, UpdateChallengeInputViewModel model);
    }
}
