using Upope.Game.Services.Models;

namespace Upope.Game.Services.Interfaces
{
    public interface IPointService
    {
        GameScore CalculatePoints(int game, bool? isWinnerHost = null);
    }
}
