using System.Threading.Tasks;
using Upope.Game.ViewModels;

namespace Upope.Game.Services.Interfaces
{
    public interface IBotService
    {
        Task AcceptChallengeRequest();
        Task SendAnswer(string accessToken, SendChoiceViewModel model);
        Task SendBluff();
        Task SendSuperBluff();
    }
}
