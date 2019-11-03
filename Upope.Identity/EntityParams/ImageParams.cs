using System;
using Upope.Identity.Data.Entities;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.Models;

namespace Upope.Identity.EntityParams
{
    public class ImageParams : IEntityParams, IImage, IOperatorFields
    {
        public ImageParams()
        {

        }
        public ImageParams(string userId, string title, string description, string imagePath)
        {
            UserId = userId;
            Title = title;
            Description = description;
            ImagePath = imagePath;
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
