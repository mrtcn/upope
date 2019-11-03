using System.Collections.Generic;
using Upope.Identity.Data.Entities;
using Upope.Identity.EntityParams;
using Upope.ServiceBase;

namespace Upope.Loyalty.Services.Interfaces
{
    public interface IImageService : IEntityServiceBase<Image>
    {
        List<ImageParams> GetImages(string userId);
    }
}
