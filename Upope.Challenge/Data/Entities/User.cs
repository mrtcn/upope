using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;

namespace Upope.Challenge.Data.Entities
{
    public interface IUser : IEntity, IHasStatus, IOperatorFields
    {
        String FirstName { get; set; }
        String LastName { get; set; }
        String Nickname { get; set; }
        String PictureUrl { get; set; }
        String UserId { get; set; }
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
        public List<ChallengeRequest> OwnedChallengeRequests { get; set; }
        public List<ChallengeRequest> ReceivedChallengeRequests { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}