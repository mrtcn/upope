
namespace Upope.Challenge.Services.Models
{
    public class ChallengeRequestModel
    {
        public int ChallengeRequestId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string UserImagePath { get; set; }
        public int Point { get; set; }
        public string Range { get; set; }
    }
}
