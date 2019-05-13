namespace Upope.Loyalty.EntityParams
{
    public class ChargeGameCreditsParams
    {
        public ChargeGameCreditsParams()
        {

        }

        public ChargeGameCreditsParams(string hostUserId, string guestUserId, int credit)
        {
            HostUserId = hostUserId;
            GuestUserId = guestUserId;
            Credit = credit;
        }

        public int Credit { get; set; }
        public string HostUserId { get; set; }
        public string GuestUserId { get; set; }
    }
}
