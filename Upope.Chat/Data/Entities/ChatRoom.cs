using System;
using System.ComponentModel.DataAnnotations;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.Chat.Data.Entities
{
    public interface IChatRoom : IEntity, IHasStatus, IDateOperationFields
    {
        string UserId { get; set; }
        string ChatUserId { get; set; }
    }

    public class ChatRoom : IChatRoom
    {
        [Key]
        public int Id { get; set; }
        public Status Status { get; set; }
        public string UserId { get; set; }
        public string ChatUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
