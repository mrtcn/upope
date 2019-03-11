using AutoMapper;
using System.Linq;
using Upope.Challenge.Data.Entities;
using Upope.Challenge.EntityParams;
using Upope.Challenge.Services.Interfaces;
using Upope.ServiceBase;

namespace Upope.Challenge.Services
{
    public class UserService : EntityServiceBase<User>, IUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public UserService(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public UserParams GetUserByUserId(string userId)
        {
            var user = Entities.FirstOrDefault(x => x.UserId == userId);
            var userParams = _mapper.Map<User, UserParams>(user);

            return userParams;
        }
    }
}
