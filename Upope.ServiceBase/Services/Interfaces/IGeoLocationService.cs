using Upope.ServiceBase.Models;

namespace Upope.ServiceBase.Services.Interfaces
{
    public interface IGeoLocationService
    {
        double GetDistance(CoordinateModel actualCoordinates, CoordinateModel destinationCoordinates);
    }
}
