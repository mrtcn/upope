using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using Upope.Identity.Enum;

namespace Upope.Identity.Models
{
    public class RegisterModel : LoginModel
    {
        [Required]
        [StringLength(200)]
        public String FirstName { get; set; }

        [Required]
        [StringLength(250)]
        public String LastName { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [Compare("Password")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public String PasswordConfirmation { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
        public UserType UserType { get; set; }
        public String ImagePath { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
