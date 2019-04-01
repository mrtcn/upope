using System.Threading.Tasks;
using Upope.Challenge.ViewModels;

namespace Upope.Challenge.Services.Interfaces
{
    public interface IGameService
    {
        Task CreateGame(CreateGameModel model, string accessToken);
    }
}
