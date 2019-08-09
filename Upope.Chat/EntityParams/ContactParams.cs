using System;
using Upope.Chat.Data.Entities;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.Models;

namespace Upope.Chat.EntityParams
{
    public class ContactParams : IEntityParams, IContact, IOperatorFields
    {
        public ContactParams()
        {

        }

        public ContactParams(string userId, string contactUserId)
        {
            UserId = userId;
            ContactUserId = contactUserId;
        }

        public int Id { get; set; }
        public Status Status { get; set; }
        public string UserId { get; set; }
        public string ContactUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
