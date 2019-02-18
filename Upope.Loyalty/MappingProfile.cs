using AutoMapper;
using Upope.Loyalty.Data.Entities;
using Upope.Loyalty.EntityParams;
using Upope.Loyalty.ViewModels;

namespace Upope.Challange
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Point, PointParams>();
            CreateMap<PointParams, Point>();

            CreateMap<PointParams, GetPointViewModel>();
            CreateMap<GetPointViewModel, PointParams>();

            CreateMap<PointParams, PointViewModel>();
            CreateMap<PointViewModel, PointParams>();

            
        }
    }
}
