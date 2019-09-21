namespace Upope.Loyalty.EntityParams
{
    public class AddScoresParams
    {
        public AddScoresParams()
        {

        }

        public AddScoresParams(string userId, int scores)
        {
            UserId = userId;
            Scores = scores;
        }

        public string UserId { get; set; }
        public int Scores { get; set; }        
    }
}
