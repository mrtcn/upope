using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upope.Identity.Entities;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.Identity.Data.Entities
{
    public interface IImage: IEntity, IHasStatus, IDateOperationFields
    {
        string UserId { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string ImagePath { get; set; }
    }
    public class Image: IImage
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }        
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
