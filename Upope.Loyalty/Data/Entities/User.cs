﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.Loyalty.Data.Entities
{
    public interface IUser : IEntity, IHasStatus, IDateOperationFields
    {
        String FirstName { get; set; }
        String LastName { get; set; }
        String Nickname { get; set; }
        String PictureUrl { get; set; }
        String UserId { get; set; }
        Gender Gender { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
        bool IsBotActivated { get; set; }
    }

    public class User : IUser
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        [Required]
        [MaxLength(200)]
        public String FirstName { get; set; }
        [Required]
        [MaxLength(250)]
        public String LastName { get; set; }
        [MaxLength(250)]
        public String Nickname { get; set; }
        public String PictureUrl { get; set; }
        public String UserId { get; set; }
        public Gender Gender { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsBotActivated { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Loyalty Loyalty { get; set; }
    }
}