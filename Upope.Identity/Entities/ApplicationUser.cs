﻿using Microsoft.AspNetCore.Identity;
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

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        [MaxLength(250)]
        public String FacebookId { get; set; }

        [MaxLength(250)]
        public String GoogleId { get; set; }

        public String PictureUrl { get; set; }

    }
}