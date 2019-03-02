using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Upope.Challange.EntityParams;
using Upope.Challange.Services.Interfaces;
using Upope.Challange.ViewModels;
using Upope.ServiceBase.Extensions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Upope.Challange.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly IUserService _userService;

        public UserController(
            IMapper mapper,
            IIdentityService identityService,
            IUserService userService)
        {
            _mapper = mapper;
            _identityService = identityService;
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        [Route("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate(CreateUserViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var userParams = _mapper.Map<CreateUserViewModel, UserParams>(model);

            var user = _userService.GetUserByUserId(userId);

            if (user != null)
                userParams.Id = user.Id;

            _userService.CreateOrUpdate(userParams);

            var result = _mapper.Map<UserParams, CreateUserViewModel>(userParams);

            return Ok(result);
        }
    }
}
