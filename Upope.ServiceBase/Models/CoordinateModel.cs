
namespace Upope.ServiceBase.Models
{
    public class CoordinateModel
    {
        public CoordinateModel(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
