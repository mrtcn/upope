using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Upope.Challange.EntityParams;
using Upope.Challange.Services.Interfaces;
using Upope.Challange.ViewModels;

namespace Upope.Challange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeService _challengeService;

        public ChallengeController(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        [HttpPost]
        [Route("Get")]
        public async Task<IActionResult> GetChallenge()
        {
            var challenge = _challengeService.Get(1);

            return Ok(challenge);
        }

        [HttpPost]
        [Route("CreateChallenge")]
        public IActionResult CreateChallenge(CreateChallengeViewModel model)
        {

            var challenge = _challengeService.CreateOrUpdate(new ChallengeParams(ServiceBase.Enums.Status.Active));

            return Ok(challenge);
        }

    }
}