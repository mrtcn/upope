namespace Upope.Loyalty.EntityParams
{
    public class ChargeCreditsParams
    {
        public ChargeCreditsParams()
        {

        }

        public ChargeCreditsParams(string userId, int credit)
        {
            UserId = userId;
            Credit = credit;
        }

        public string UserId { get; set; }
        public int Credit { get; set; }        
    }
}
