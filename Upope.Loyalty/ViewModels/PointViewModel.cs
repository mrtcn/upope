
namespace Upope.Loyalty.ViewModels
{
    public class PointViewModel
    {
        public int Point { get; set; }
        public string UserId { get; set; }
    }

    public class AddPointViewModel
    {
        public int PointToBeAdded { get; set; }
        public string UserId { get; set; }
    }

    public class GetPointViewModel
    {
        public int Point { get; set; }
    }

    public class GetSufficientPointViewModel
    {
        public int Point { get; set; }
    }
}
