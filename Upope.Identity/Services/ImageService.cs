using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Upope.Identity.Data.Entities;
using Upope.Identity.DbContext;
using Upope.Identity.EntityParams;
using Upope.Loyalty.Services.Interfaces;
using Upope.ServiceBase;

namespace Upope.Loyalty.Services
{
    public class ImageService : EntityServiceBase<Image>, IImageService
    {
        private readonly IMapper _mapper;

        public ImageService(
            ApplicationUserDbContext applicationDbContext, 
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _mapper = mapper;
        }

        public List<ImageParams> GetImages(string userId)
        {
            var images = Entities.Where(x => x.UserId == userId && x.Status == ServiceBase.Enums.Status.Active);
            var imageParamsList = _mapper.Map<List<ImageParams>>(images);

            return imageParamsList;
        }
    }
}
