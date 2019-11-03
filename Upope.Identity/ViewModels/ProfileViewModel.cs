using System;
using Upope.ServiceBase.Enums;

namespace Upope.Identity.ViewModels
{
    public class ProfileViewModel
    {
        public String Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Gender Gender { get; set; }
        public String PictureUrl { get; set; }
        public String LargePictureUrl { get; set; }
        public DateTime? Birthday { get; set; }
        public int CurrentWinStreak { get; set; }
        public int WinRecord { get; set; }
        public int Credit { get; set; }
        public int Score { get; set; }
        public UserType UserType { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
