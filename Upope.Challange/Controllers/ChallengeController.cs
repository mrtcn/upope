using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Upope.Challange.Services.Interfaces;

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

    }
}