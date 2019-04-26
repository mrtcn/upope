using GeoCoordinatePortable;

namespace Upope.Challenge.Services.Interfaces
{
    public interface IGeoLocationService
    {
        double GetDistance(GeoCoordinate actualCoordinates, GeoCoordinate destinationCoordinates);
    }
}
