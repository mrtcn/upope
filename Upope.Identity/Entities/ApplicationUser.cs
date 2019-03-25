using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using Upope.Identity.Enum;

namespace Upope.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(200)]
        public String FirstName { get; set; }

        [Required]
        [MaxLength(250)]
        public String LastName { get; set; }

        [MaxLength(250)]
        public String Nickname { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        [MaxLength(250)]
        public String FacebookId { get; set; }

        [MaxLength(250)]
        public String GoogleId { get; set; }

        public String PictureUrl { get; set; }

        public DateTime? Birthday { get; set; }
        public UserType UserType { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public String RefreshToken { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
