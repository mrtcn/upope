using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Upope.Challenge.EntityParams;
using Upope.Challenge.Services.Interfaces;
using Upope.Challenge.ViewModels;
using Upope.ServiceBase.Extensions;
using Upope.ServiceBase.Helpers;
using Upope.ServiceBase.Services.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Upope.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomControllerBase
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

            try
            {
                var userParams = _mapper.Map<CreateUserViewModel, UserParams>(model);
                userParams.UserId = userId;

                var user = _userService.GetUserByUserId(userId);

                if (user != null)
                    userParams.Id = user.Id;

                _userService.CreateOrUpdate(userParams);

                var result = _mapper.Map<UserParams, CreateUserViewModel>(userParams);

                return Ok(result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
