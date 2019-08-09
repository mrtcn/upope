using AutoMapper;
using System.Linq;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.Game.Services.Interfaces;
using Upope.ServiceBase;

namespace Upope.Game.Services
{
    public class UserService : EntityServiceBase<User>, IUserService
    {
        private readonly IMapper _mapper;

        public UserService(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper) : base(applicationDbContext, mapper)
        {
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
