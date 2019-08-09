using System;
using Upope.Chat.Data.Entities;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.Models;

namespace Upope.Chat.EntityParams
{
    public class ChatParams : IEntityParams, IChat, IOperatorFields
    {
        public ChatParams()
        {

        }

        public ChatParams(string accessToken, int chatRoomId, string userId, string message)
        {
            AccessToken = accessToken;
            ChatRoomId = chatRoomId;
            UserId = userId;
            Message = message;
        }

        public ChatParams(string accessToken, int chatRoomId)
        {
            AccessToken = accessToken;
            ChatRoomId = chatRoomId;
        }

        public int Id { get; set; }
        public Status Status { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public int ChatRoomId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
