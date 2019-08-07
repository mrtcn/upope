using System;
using Upope.Chat.Data.Entities;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.Models;

namespace Upope.Chat.EntityParams
{
    public class ChatRoomParams : IEntityParams, IChatRoom, IOperatorFields
    {
        public ChatRoomParams()
        {

        }

        public ChatRoomParams(string accessToken, string chatUserId)
        {
            AccessToken = accessToken;
            ChatUserId = chatUserId;
        }

        public int Id { get; set; }
        public Status Status { get; set; }
        public string AccessToken { get; set; }
        public string UserId { get; set; }
        public string ChatUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
