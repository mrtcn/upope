using GeoCoordinatePortable;
using Upope.ServiceBase.Models;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.Challenge.Services
{
    public class GeoLocationService: IGeoLocationService
    {
        public double GetDistance(CoordinateModel actualCoordinates, CoordinateModel destinationCoordinates)
        {
            var actualGeoCoordinate = new GeoCoordinate(actualCoordinates.Latitude, actualCoordinates.Longitude);
            var destinationGeoCoordinate = new GeoCoordinate(destinationCoordinates.Latitude, destinationCoordinates.Longitude);

            return actualGeoCoordinate.GetDistanceTo(destinationGeoCoordinate);
        }
    }
}
