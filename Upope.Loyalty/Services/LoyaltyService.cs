using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Upope.Loyalty.Data.Entities;
using Upope.Loyalty.EntityParams;
using Upope.Loyalty.Services.Interfaces;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;

namespace Upope.Loyalty.Services
{    
    public class LoyaltyService : EntityServiceBase<Point>, ILoyaltyService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public LoyaltyService(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public PointParams GetPointByUserId(string userId)
        {
            var point = Entities.FirstOrDefault(x => x.UserId == userId && x.Status == Status.Active);
            var pointParams = _mapper.Map<PointParams>(point);

            return pointParams;
        }

        public List<PointParams> GetSufficientPoints(int point)
        {
            var sufficientPoints = Entities.FirstOrDefault(x => x.Points >= point && x.Status == Status.Active);
            var pointParams = _mapper.Map<List<PointParams>>(sufficientPoints);

            return pointParams;
        }
    }
}
