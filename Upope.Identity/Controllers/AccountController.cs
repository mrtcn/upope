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
using Upope.ServiceBase.Extensions;

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
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ApplicationUserDbContext _dbContext;
        private readonly IMapper _mapper;
        readonly IRandomPasswordHelper _randomPasswordHelper;
        readonly IExternalAuthService<FacebookResponse> _facebookService;
        readonly IExternalAuthService<GoogleResponse> _googleService;
        readonly IConfiguration configuration;
        readonly ILogger<AccountController> logger;
        private readonly IChallengeUserSyncService _challengeUserSyncService;
        private readonly ILoyaltySyncService _loyaltySyncService;
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
           ITokenService tokenService,
           IPasswordHasher passwordHasher,
           ApplicationUserDbContext dbContext,
           IMapper mapper,
           IRandomPasswordHelper randomPasswordHelper,
           IExternalAuthService<FacebookResponse> facebookService,
           IExternalAuthService<GoogleResponse> googleService,
           IConfiguration configuration,
           ILogger<AccountController> logger,
           IChallengeUserSyncService challengeUserSyncService,
           ILoyaltySyncService loyaltySyncService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _tokenService = tokenService;
            _dbContext = dbContext;
            _mapper = mapper;
            _randomPasswordHelper = randomPasswordHelper;
            _facebookService = facebookService;
            _googleService = googleService;
            this.configuration = configuration;
            this.logger = logger;
            _challengeUserSyncService = challengeUserSyncService;
            _loyaltySyncService = loyaltySyncService;
            TokenLifetime = DateTime.UtcNow.AddSeconds(this.configuration.GetValue<int>("Tokens:Lifetime"));
            TokenAudience = configuration.GetValue<String>("Tokens:Audience");
            TokenIssuer = configuration.GetValue<String>("Tokens:Issuer");
            TokenKey = configuration.GetValue<String>("Tokens:Key");
            AppId = configuration.GetValue<String>("ExternalAuthentication:Facebook:AppId");
            AppSecret = configuration.GetValue<String>("ExternalAuthentication:Facebook:AppSecret");
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        [HttpPost]
        [Route("getuserid")]
        [Authorize]
        public async Task<IActionResult> GetUserId()
        {
            var user = await GetCurrentUserAsync();
            return Ok(user.Id);
        }

        [HttpPost]
        [Route("anon/login")]
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

                var accessToken = _tokenService.GenerateAccessToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                await userManager.UpdateAsync(user);

                return Ok(new TokenModel(accessToken, refreshToken));
            }
            return BadRequest(ModelState);

        }

        [Authorize]
        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken(TokenModel model)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(model.AccessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            var user = await userManager.FindByNameAsync(
                username ??
                User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault()
                );

            if (user == null || user.RefreshToken != model.RefreshToken) return BadRequest();

            var newJwtToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await userManager.UpdateAsync(user);

            return Ok(new TokenModel(newJwtToken, newRefreshToken));

        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Revoke()
        {
            var username = User.Identity.Name;

            var user = await userManager.FindByNameAsync(
               username ??
               User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault()
               );

            if (user == null) return BadRequest();

            user.RefreshToken = null;

            await userManager.UpdateAsync(user);

            return NoContent();
        }

        [HttpPost]
        [Route("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await GetUserAsync();

            var profileViewModel = _mapper.Map<ProfileViewModel>(user);

            return Ok(profileViewModel);
        }

        [HttpPost]
        [Route("anon/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            try
            {
                var refreshToken = _tokenService.GenerateRefreshToken();

                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        //TODO: Use Automapper instaed of manual binding

                        UserName = registerModel.Username,
                        FirstName = registerModel.FirstName,
                        LastName = registerModel.LastName,
                        Email = registerModel.Email,
                        UserType = registerModel.UserType,
                        Birthday = registerModel.Birthday,
                        Latitute = registerModel.Latitute,
                        Longitude = registerModel.Longitude,
                        RefreshToken = refreshToken
                    };

                    var identityResult = await userManager.CreateAsync(user, registerModel.Password);
                    if (identityResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);

                        var accessToken = _tokenService.GenerateAccessToken(user);
                        // Syncing the Challenge DB User table
                        await SyncChallengeUserTable(user, accessToken);

                        // Syncing the Loyalty DB Loyalty table
                        await SyncLoyaltyTable(true, user, accessToken);

                        return Ok(new TokenModel(accessToken, refreshToken));
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
        [Route("anon/facebook")]
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
                var isCreate = user == null;

                var refreshToken = _tokenService.GenerateRefreshToken();

                if (isCreate)
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
                        Gender = facebookUser.Gender.TryConvertToEnum<Gender>().GetValueOrDefault(),
                        Latitute = model.Latitude,
                        Longitude = model.Longitude,
                        RefreshToken = refreshToken
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

                var accessToken = _tokenService.GenerateAccessToken(user);                
                
                // Syncing the Challenge DB User table
                await SyncChallengeUserTable(localUser, accessToken);

                // Syncing the Loyalty DB Loyalty table
                await SyncLoyaltyTable(isCreate, localUser, accessToken);

                return Ok(new TokenModel(accessToken, refreshToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        private async Task SyncChallengeUserTable(ApplicationUser localUser, string accessToken)
        {
            // Syncing the Challenge DB User table
            var createChallengeUserViewModel = _mapper.Map<ApplicationUser, CreateOrUpdateChallengeUserViewModel>(localUser);
            createChallengeUserViewModel.UserId = localUser.Id;

            await _challengeUserSyncService.SyncChallengeUserTable(createChallengeUserViewModel, accessToken);
        }

        private async Task SyncLoyaltyTable(bool isCreate, ApplicationUser localUser, string accessToken)
        {
            if (isCreate)
            {
                var createOrUpdateLoyaltyViewModel = new CreateOrUpdateLoyaltyViewModel()
                {
                    UserId = localUser.Id,
                    Credit = 50,
                    Score = 0,
                    Win = 0
                };
                await _loyaltySyncService.SyncLoyaltyTable(createOrUpdateLoyaltyViewModel, accessToken);

            }
        }

        // POST api/externalauth/google
        [HttpPost]
        [Route("anon/google")]
        public async Task<IActionResult> Google([FromBody]GoogleAuthViewModel model)
        {
            var googleUser = await _googleService.GetAccountAsync(model.AccessToken);

            if (string.IsNullOrEmpty(googleUser.Sub))
            {
                return BadRequest("Invalid google token.");
            }

            // 4. ready to create the local user account (if necessary) and jwt
            var user = GetUserByExternalId(googleUser.Sub, ExternalProviderTyper.Google);
            var isCreate = user == null;
            var refreshToken = _tokenService.GenerateRefreshToken();

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
                    PictureUrl = googleUser.Picture,
                    Latitute = model.Latitude,
                    Longitude = model.Longitude,
                    Birthday = DateTime.ParseExact(googleUser.Birthday, "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    RefreshToken = refreshToken
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

            var accessToken = _tokenService.GenerateAccessToken(user);

            // Syncing the Challenge DB User table
            await SyncChallengeUserTable(localUser, accessToken);

            // Syncing the Loyalty DB Loyalty table
            await SyncLoyaltyTable(isCreate, localUser, accessToken);

            return Ok(new TokenModel(accessToken, refreshToken));
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

        private async Task<ApplicationUser> GetUserAsync()
        {
            var user = await userManager.FindByNameAsync(
                User.Identity.Name ??
                User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault());

            return user;
        }
    }
}