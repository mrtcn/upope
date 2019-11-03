using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Upope.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly ILogger<UserManagementController> _logger;

        public UserManagementController(ILogger<UserManagementController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("userprofile")]
        public IActionResult UserProfile()
        {
            return Ok(true);
        }

        [HttpPost]
        [Authorize]
        [Route("userprofileauth")]
        public IActionResult UserProfileAuth()
        {
            return Ok(true);
        }

        // POST api/values
        [HttpPost]
        [Route("ProcessDataCallback")]
        public void ProcessDataCallback(object data)
        {
        }
    }
}