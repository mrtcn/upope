using GeoCoordinatePortable;
using Upope.Challenge.Services.Interfaces;

namespace Upope.Challenge.Services
{
    public class GeoLocationService: IGeoLocationService
    {
        public double GetDistance(GeoCoordinate actualCoordinates, GeoCoordinate destinationCoordinates)
        {
            return actualCoordinates.GetDistanceTo(destinationCoordinates);
        }
    }
}
