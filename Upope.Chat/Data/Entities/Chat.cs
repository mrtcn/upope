using System;
using System.ComponentModel.DataAnnotations;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.Chat.Data.Entities
{
    public interface IChat : IEntity, IHasStatus, IDateOperationFields
    {
        string Message { get; set; }
        string UserId { get; set; }
        int ChatRoomId { get; set; }
    }

    public class Chat : IChat
    {
        [Key]
        public int Id { get; set; }
        public Status Status { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public int ChatRoomId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}
