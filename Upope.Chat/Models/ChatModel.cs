
using System;

namespace Upope.Chat.Models
{
    public class ChatModel
    {
        public string Text { get; set; }
        public string UserId { get; set; }
        public int ChatRoomId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
