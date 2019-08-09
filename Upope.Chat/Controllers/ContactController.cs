using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Upope.Chat.EntityParams;
using Upope.Chat.Services.Interfaces;
using Upope.ServiceBase.Extensions;
using Upope.ServiceBase.ServiceBase.Models;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IIdentityService _identityService;

        public ContactController(
            IContactService contactService,
            IIdentityService identityService)
        {
            _contactService = contactService;
            _identityService = identityService;
        }

        [HttpPost("Contact/{contactUserId}")]
        public async Task<IActionResult> CreateContact(string contactUserId)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            _contactService.CreateOrUpdate(new ContactParams(userId, contactUserId));

            return Ok();
        }

        [HttpDelete("Contact/{contactUserId}")]
        public async Task<IActionResult> RemoveContact(string contactUserId)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var contactParams = _contactService.GetContact(userId, contactUserId);
            _contactService.Remove(new RemoveEntityParams(contactParams.Id, null, true, false));

            return Ok();
        }
    }
}