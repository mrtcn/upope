
namespace Upope.Challenge.Services.Models
{
    public class CreateOrUpdateGameModel
    {
        public int Id { get; set; }
        public string HostUserId { get; set; }
        public string GuestUserId { get; set; }
        public int Credit { get; set; }
    }
}
