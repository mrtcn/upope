using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text.RegularExpressions;
using Upope.Identity.Enum;
using Upope.Identity.Helpers;
using Upope.Identity.Models.FacebookResponse;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Extensions;

namespace Upope.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }

        public ApplicationUser(FacebookResponse facebookUser, string refreshToken, string projectPath)
        {
            FirstName = facebookUser.FirstName;
            LastName = facebookUser.LastName;
            FacebookId = facebookUser.Id;
            Email = facebookUser.Email;
            Nickname = Regex.Replace(facebookUser.Name, @"[^\w]", "").ToLower();
            UserName = Guid.NewGuid().ToString();
            PictureUrl = SaveImageUrlToDisk.SaveImage(facebookUser.Picture.Data.Url, projectPath, ImageFormat.Png);
            Birthday = DateTime.ParseExact(facebookUser.Birthday, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            Gender = facebookUser.Gender.TryConvertToEnum<Gender>().GetValueOrDefault();
            RefreshToken = refreshToken;
        }

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
        public String LargePictureUrl { get; set; }

        public DateTime? Birthday { get; set; }
        public UserType UserType { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public String RefreshToken { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
