using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upope.Identity.Enum;

namespace Upope.Identity.ViewModels
{
    public class ProfileViewModel
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Gender Gender { get; set; }
        public String PictureUrl { get; set; }
        public DateTime? Birthday { get; set; }
        public int Point { get; set; }
    }
}
