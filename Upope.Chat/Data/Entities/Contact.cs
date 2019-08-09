using System;
using System.ComponentModel.DataAnnotations;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.Chat.Data.Entities
{
    public interface IContact : IEntity, IHasStatus, IDateOperationFields
    {
        string UserId { get; set; }
        string ContactUserId { get; set; }
    }

    public class Contact : IContact
    {
        [Key]
        public int Id { get; set; }
        public Status Status { get; set; }
        public string UserId { get; set; }
        public string ContactUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}