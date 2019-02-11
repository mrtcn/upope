using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Upope.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {

        [HttpPost]
        [Route("userprofile")]
        public async Task<IActionResult> UserProfile()
        {            
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("userprofileauth")]
        public async Task<IActionResult> UserProfileAuth()
        {
            return Ok();
        }
    }
}