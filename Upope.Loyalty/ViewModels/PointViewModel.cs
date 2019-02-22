
namespace Upope.Loyalty.ViewModels
{
    public class PointViewModel
    {
        public int Points { get; set; }
        public string UserId { get; set; }
    }

    public class AddPointViewModel
    {
        public int PointToBeAdded { get; set; }
        public string UserId { get; set; }
    }

    public class GetPointViewModel
    {
        public int Points { get; set; }
    }

    public class GetSufficientPointViewModel
    {
        public int Points { get; set; }
    }
}
