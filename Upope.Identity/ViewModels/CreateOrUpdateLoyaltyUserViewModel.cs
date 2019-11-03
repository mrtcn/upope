using System;
using Upope.ServiceBase.Enums;

namespace Upope.Identity.ViewModels
{
    public class CreateOrUpdateLoyaltyUserViewModel
    {
        public String UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public String PictureUrl { get; set; }
        public Gender Gender { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsBotActivated { get; set; }
    }
}
