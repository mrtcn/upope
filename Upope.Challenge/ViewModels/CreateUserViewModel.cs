using System;

namespace Upope.Challenge.ViewModels
{
    public class CreateUserViewModel
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public String PictureUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
