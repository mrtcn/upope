using AutoMapper;
using System.Linq;
using Upope.Chat.Data.Entities;
using Upope.Chat.EntityParams;
using Upope.Chat.Services.Interfaces;
using Upope.ServiceBase;

namespace Upope.Chat.Services
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
