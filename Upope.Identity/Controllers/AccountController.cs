using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Upope.Identity.DbContext;
using Upope.Identity.Entities;
using Upope.Identity.Enum;
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
        private readonly ApplicationUserDbContext _dbContext;
        private readonly IMapper _mapper;
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
           ApplicationUserDbContext dbContext,
           IMapper mapper,
           IRandomPasswordHelper randomPasswordHelper,
           IExternalAuthService<FacebookResponse> facebookService,
           IExternalAuthService<GoogleResponse> googleService,
           IConfiguration configuration,
           ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _dbContext = dbContext;
            _mapper = mapper;
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

                return Ok(new TokenModel(GetToken(user)));
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
            return Ok(new TokenModel(GetToken(user)));

        }

        [HttpPost]
        [Route("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await GetUserAsync();
            var profileViewModel = _mapper.Map<ProfileViewModel>(user);
            profileViewModel.Point = 112;
            return Ok(profileViewModel);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            try
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
                        return Ok(new TokenModel(GetToken(user)));
                    }
                    else
                    {
                        return BadRequest(identityResult.Errors);
                    }
                }
                return BadRequest(ModelState);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

        }

        // POST api/externalauth/facebook
        [HttpPost]
        [Route("facebook")]
        public async Task<IActionResult> Facebook([FromBody]FacebookAuthViewModel model)
        {
            try
            {
                var facebookUser = await _facebookService.GetAccountAsync(model.AccessToken);

                if (string.IsNullOrEmpty(facebookUser.Id))
                {
                    return BadRequest("Invalid facebook token.");
                }

                // 4. ready to create the local user account (if necessary) and jwt
                var user = GetUserByExternalId(facebookUser.Id, ExternalProviderTyper.Facebook);

                if (user == null)
                {
                    var appUser = new ApplicationUser
                    {
                        FirstName = facebookUser.FirstName,
                        LastName = facebookUser.LastName,
                        FacebookId = facebookUser.Id,
                        Email = facebookUser.Email,
                        Nickname = Regex.Replace(facebookUser.Name, @"[^\w]", ""),
                        UserName = Guid.NewGuid().ToString(),
                        PictureUrl = facebookUser.Picture.Data.Url,
                        Birthday = DateTime.ParseExact(facebookUser.Birthday, "MM/dd/yyyy", CultureInfo.InvariantCulture),
                        //Gender = facebookUser.Gender
                    };
                    if (!IsEmailUnique(appUser.Email))
                        return BadRequest("Email baska bir kullaniciya ait.");

                    var result = await userManager.CreateAsync(appUser, _randomPasswordHelper.GenerateRandomPassword());

                    if (!result.Succeeded)
                    {
                        return new BadRequestObjectResult(result.Errors.FirstOrDefault());
                    }
                }

                // generate the jwt for the local user...
                var localUser = GetUserByExternalId(facebookUser.Id, ExternalProviderTyper.Facebook);

                if (localUser == null)
                {
                    return BadRequest("Failed to create local user account.");
                }

                return Ok(new TokenModel(GetToken(localUser)));
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
        }


        // POST api/externalauth/google
        [HttpPost]
        [Route("google")]
        public async Task<IActionResult> Google([FromBody]GoogleAuthViewModel model)
        {
            var googleUser = await _googleService.GetAccountAsync(model.AccessToken);

            if (string.IsNullOrEmpty(googleUser.Sub))
            {
                return BadRequest("Invalid google token.");
            }

            // 4. ready to create the local user account (if necessary) and jwt
            var user = GetUserByExternalId(googleUser.Sub, ExternalProviderTyper.Google);

            if (user == null)
            {
                var appUser = new ApplicationUser
                {
                    FirstName = googleUser.GivenName,
                    LastName = googleUser.FamilyName,
                    GoogleId = googleUser.Sub,
                    Email = googleUser.Email,
                    Nickname = Regex.Replace(googleUser.Name, @"[^\w]", ""),
                    UserName = Guid.NewGuid().ToString(),
                    PictureUrl = googleUser.Picture
                    //Birthday = DateTime.ParseExact(googleUser.Birthday, "MM/dd/yyyy", CultureInfo.InvariantCulture)
                };

                if (!IsEmailUnique(appUser.Email))
                    return BadRequest("Email baska bir kullaniciya ait.");

                var result = await userManager.CreateAsync(appUser, _randomPasswordHelper.GenerateRandomPassword());

                if (!result.Succeeded)
                {
                    return new BadRequestObjectResult(result.Errors.FirstOrDefault());
                }
            }

            // generate the jwt for the local user...
            var localUser = GetUserByExternalId(googleUser.Sub, ExternalProviderTyper.Google);

            if (localUser == null)
            {
                return BadRequest("Failed to create local user account.");
            }

            return Ok(new TokenModel(GetToken(localUser)));
        }

        private ApplicationUser GetUserByExternalId(string externalId, ExternalProviderTyper providerType)
        {
            if (string.IsNullOrEmpty(externalId))
                return null;

             if(providerType == ExternalProviderTyper.Facebook)
            {
                return _dbContext.Users.FirstOrDefault(x => x.FacebookId == externalId);
            }
            else
            {
                return _dbContext.Users.FirstOrDefault(x => x.GoogleId == externalId);
            }
        }

        private bool IsEmailUnique(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return true;
            }
            return !_dbContext.Users.Any(x => x.Email == email);
            
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

        private async Task<ApplicationUser> GetUserAsync()
        {
            var user = await userManager.FindByNameAsync(
                User.Identity.Name ??
                User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault());

            return user;
        }
    }
}