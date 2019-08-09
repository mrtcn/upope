using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upope.ServiceBase.Enums;

namespace Upope.Identity.ViewModels
{
    public class CreateOrUpdateGameUserViewModel
    {
        public String UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public String PictureUrl { get; set; }
        public Gender Gender { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
