using System;
using Upope.Challange.Data.Entities;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Models;

namespace Upope.Challange.EntityParams
{
    public class UserParams: IEntityParams, IUser
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public String PictureUrl { get; set; }
        public String UserId { get; set; }
    }
}
