using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Upope.Identity.Entities;
using Upope.Identity.Helpers.Interfaces;
using Upope.Identity.Models;
using Upope.Identity.Models.FacebookResponse;
using Upope.Identity.Models.GoogleResponse;
using Upope.Identity.Services.Interfaces;
using Upope.Identity.ViewModels;

namespace Upope.Identity.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        readonly UserManager<ApplicationUser> userManager;
        readonly SignInManager<ApplicationUser> signInManager;
        readonly IRandomPasswordHelper _randomPasswordHelper;
        readonly IExternalAuthService<FacebookResponse> _facebookService;
        readonly IExternalAuthService<GoogleResponse> _googleService;
        readonly IConfiguration configuration;
        readonly ILogger<AccountController> logger;
        readonly DateTime? TokenLifetime;
        readonly string TokenAudience;
        readonly string TokenIssuer;
        readonly string TokenKey;
        readonly string AppId;
        readonly string AppSecret;

        static HttpClient client = new HttpClient();

        public AccountController(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           IRandomPasswordHelper randomPasswordHelper,
           IExternalAuthService<FacebookResponse> facebookService,
           IExternalAuthService<GoogleResponse> googleService,
           IConfiguration configuration,
           ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _randomPasswordHelper = randomPasswordHelper;
            _facebookService = facebookService;
            _googleService = googleService;
            this.configuration = configuration;
            this.logger = logger;
            TokenLifetime = DateTime.UtcNow.AddSeconds(this.configuration.GetValue<int>("Tokens:Lifetime"));
            TokenAudience = configuration.GetValue<String>("Tokens:Audience");
            TokenIssuer = configuration.GetValue<String>("Tokens:Issuer");
            TokenKey = configuration.GetValue<String>("Tokens:Key");
            AppId = configuration.GetValue<String>("ExternalAuthentication:Facebook:AppId");
            AppSecret = configuration.GetValue<String>("ExternalAuthentication:Facebook:AppSecret");
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginModel.Username);

                if(user == null)
                {
                    user = await userManager.FindByEmailAsync(loginModel.Username);
                }

                if(user == null)
                {
                    return BadRequest();
                }

                var loginResult = await signInManager.PasswordSignInAsync(user.UserName, loginModel.Password, isPersistent: false, lockoutOnFailure: false);

                if (!loginResult.Succeeded)
                {
                    return BadRequest();
                }

                return Ok(GetToken(user));
            }
            return BadRequest(ModelState);

        }

        [Authorize]
        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            var user = await userManager.FindByNameAsync(
                User.Identity.Name ??
                User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault()
                );
            return Ok(GetToken(user));

        }


        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    //TODO: Use Automapper instaed of manual binding

                    UserName = registerModel.Username,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    Email = registerModel.Email
                };

                var identityResult = await userManager.CreateAsync(user, registerModel.Password);
                if (identityResult.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(GetToken(user));
                }
                else
                {
                    return BadRequest(identityResult.Errors);
                }
            }
            return BadRequest(ModelState);


        }

        // POST api/externalauth/facebook
        [HttpPost]
        [Route("facebook")]
        public async Task<IActionResult> Facebook([FromBody]FacebookAuthViewModel model)
        {
            var facebookUser = await _facebookService.GetAccountAsync(model.AccessToken);

            if (facebookUser == null || string.IsNullOrEmpty(facebookUser.Id))
            {
                return BadRequest("Invalid facebook token.");
            }

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await userManager.FindByEmailAsync(facebookUser.Email);

            if (user == null)
            {
                var appUser = new ApplicationUser
                {
                    FirstName = facebookUser.FirstName,
                    LastName = facebookUser.LastName,
                    FacebookId = facebookUser.Id,
                    Email = facebookUser.Email,
                    UserName = Regex.Replace(facebookUser.Name, @"[^\w]", ""),
                    PictureUrl = facebookUser.Picture.Data.Url
                };

                var result = await userManager.CreateAsync(appUser, _randomPasswordHelper.GenerateRandomPassword());
                
                if (!result.Succeeded) {
                    return new BadRequestObjectResult(result.Errors.FirstOrDefault());
                }                
            }

            // generate the jwt for the local user...
            var localUser = await userManager.FindByEmailAsync(facebookUser.Email);

            if (localUser == null)
            {
                return BadRequest("Failed to create local user account.");
            }

            return Ok(new TokenModel(GetToken(localUser)));
        }


        // POST api/externalauth/google
        [HttpPost]
        [Route("google")]
        public async Task<IActionResult> Google([FromBody]GoogleAuthViewModel model)
        {
            var googleUser = await _googleService.GetAccountAsync(model.UserId);

            if (googleUser == null && string.IsNullOrEmpty(googleUser.Sub))
            {
                return BadRequest("Invalid google token.");
            }

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await userManager.FindByEmailAsync(googleUser.Email);

            if (user == null)
            {
                var appUser = new ApplicationUser
                {
                    FirstName = googleUser.GivenName,
                    LastName = googleUser.FamilyName,
                    GoogleId = googleUser.Sub,
                    Email = googleUser.Email,
                    UserName = Regex.Replace(googleUser.Name, @"[^\w]", ""),
                    PictureUrl = googleUser.Picture
                };
                
                var result = await userManager.CreateAsync(appUser, _randomPasswordHelper.GenerateRandomPassword());

                if (!result.Succeeded)
                {
                    return new BadRequestObjectResult(result.Errors.FirstOrDefault());
                }
            }

            // generate the jwt for the local user...
            var localUser = await userManager.FindByNameAsync(googleUser.Email);

            if (localUser == null)
            {
                return BadRequest("Failed to create local user account.");
            }

            return Ok(new TokenModel(GetToken(localUser)));
        }

        private String GetToken(IdentityUser user)
            {
                var utcNow = DateTime.UtcNow;

                var claims = new Claim[]
                {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
                };

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey));
                var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                var jwt = new JwtSecurityToken(
                    signingCredentials: signingCredentials,
                    claims: claims,
                    notBefore: utcNow,
                    expires: TokenLifetime,
                    audience: TokenAudience,
                    issuer: TokenIssuer
                    );

                return new JwtSecurityTokenHandler().WriteToken(jwt);

            }

        }
}