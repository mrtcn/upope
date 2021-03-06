﻿using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using ImageMagick;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Upope.Identity.DbContext;
using Upope.Identity.Entities;
using Upope.Identity.EntityParams;
using Upope.Identity.Enum;
using Upope.Identity.Helpers;
using Upope.Identity.Helpers.Interfaces;
using Upope.Identity.Models;
using Upope.Identity.Models.FacebookResponse;
using Upope.Identity.Models.GoogleResponse;
using Upope.Identity.Services.Interfaces;
using Upope.Identity.ViewModels;
using Upope.Loyalty.Services.Interfaces;
using Upope.ServiceBase.Extensions;
using Upope.ServiceBase.Helpers;

namespace Upope.Identity.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : CustomControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ApplicationUserDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IRandomPasswordHelper _randomPasswordHelper;
        private readonly IExternalAuthService<FacebookResponse> _facebookService;
        private readonly IExternalAuthService<GoogleResponse> _googleService;
        private readonly ILogger<AccountController> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IChallengeUserSyncService _challengeUserSyncService;
        private readonly ILoyaltySyncService _loyaltySyncService;
        private readonly ILoyaltyService _loyaltyService;
        private readonly IGameUserSyncService _gameUserSyncService;
        private readonly IImageService _imageService;
        private readonly IStringLocalizer<AccountController> _localizer;

        public AccountController(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           ITokenService tokenService,
           ApplicationUserDbContext dbContext,
           IMapper mapper,
           IRandomPasswordHelper randomPasswordHelper,
           IExternalAuthService<FacebookResponse> facebookService,
           IExternalAuthService<GoogleResponse> googleService,
           ILogger<AccountController> logger,
           IHostingEnvironment hostingEnvironment,
           IChallengeUserSyncService challengeUserSyncService,
           ILoyaltySyncService loyaltySyncService,
           ILoyaltyService loyaltyService,
           IGameUserSyncService gameUserSyncService,
           IImageService imageService,
           IStringLocalizer<AccountController> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _dbContext = dbContext;
            _mapper = mapper;
            _randomPasswordHelper = randomPasswordHelper;
            _facebookService = facebookService;
            _googleService = googleService;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _challengeUserSyncService = challengeUserSyncService;
            _loyaltySyncService = loyaltySyncService;
            _loyaltyService = loyaltyService;
            _gameUserSyncService = gameUserSyncService;
            _imageService = imageService;
            _localizer = localizer;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpGet("getuserid")]
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
                var user = await _userManager.FindByNameAsync(loginModel.Username);

                if(user == null)
                {
                    user = await _userManager.FindByEmailAsync(loginModel.Username);
                }

                if(user == null)
                {
                    return BadRequest(_localizer.GetString("UserNotFound"));
                }

                var loginResult = await _signInManager.PasswordSignInAsync(user.UserName, loginModel.Password, isPersistent: false, lockoutOnFailure: false);

                if (!loginResult.Succeeded)
                {
                    return BadRequest(_localizer.GetString("UsernameOrPasswordWrong"));
                }

                var accessToken = _tokenService.GenerateAccessToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                await _userManager.UpdateAsync(user);

                return Ok(new TokenModel(accessToken, refreshToken));
            }
            return BadRequest(_localizer.GetString("LoginFailed"));
        }

        [Authorize]
        [HttpPut("refreshtoken")]
        public async Task<IActionResult> RefreshToken(TokenModel model)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(model.AccessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            var user = await _userManager.FindByNameAsync(
                username ??
                User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault()
                );

            if (user == null || user.RefreshToken != model.RefreshToken) return BadRequest();

            var newJwtToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await _userManager.UpdateAsync(user);

            return Ok(new TokenModel(newJwtToken, newRefreshToken));

        }

        [HttpPut("revoke"), Authorize]
        public async Task<IActionResult> Revoke()
        {
            var username = User.Identity.Name;

            var user = await _userManager.FindByNameAsync(
               username ??
               User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault()
               );

            if (user == null) return BadRequest();

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        [HttpGet]
        [Route("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var user = await GetUserAsync();

            var profileViewModel = _mapper.Map<ProfileViewModel>(user);
            var userStats = await _loyaltyService.GetLoyalty(accessToken, user.Id);
            profileViewModel.Score = userStats.Score;
            profileViewModel.Credit = userStats.Credit;
            profileViewModel.WinRecord = userStats.WinRecord;
            profileViewModel.CurrentWinStreak = userStats.CurrentWinStreak;

            return Ok(profileViewModel);
        }

        [HttpGet]
        [Route("userProfile/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);

            var profileViewModel = _mapper.Map<ProfileViewModel>(user);

            var userStats = await _loyaltyService.GetLoyalty(accessToken, user.Id);
            profileViewModel.Score = userStats.Score;
            profileViewModel.Credit = userStats.Credit;
            profileViewModel.WinRecord = userStats.WinRecord;
            profileViewModel.CurrentWinStreak = userStats.CurrentWinStreak;

            return Ok(profileViewModel);
        }

        [HttpPut("UpdateLocation")]
        [Authorize]
        public async Task<IActionResult> UpdateLocation([FromBody] LocationViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var user = await GetUserAsync();
            user.Latitude = model.Latitude;
            user.Longitude = model.Longitude;

            var updatedUser = await _userManager.UpdateAsync(user);
            if (updatedUser.Succeeded)
            {
                // Syncing the Challenge DB User table
                await SyncChallengeUserTable(user, accessToken);

                // Syncing the Loyalty DB User table
                await SyncLoyaltyUserTable(false, user, accessToken);

                // Syncing the Game DB User table
                await SyncGameUserTable(user, accessToken);
            }
                

            return Ok(true);
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
                        Gender = registerModel.Gender,
                        UserType = registerModel.UserType,
                        Birthday = registerModel.Birthday,
                        Latitude = registerModel.Latitude,
                        Longitude = registerModel.Longitude,
                        RefreshToken = refreshToken,
                        PictureUrl = registerModel.ImagePath,
                        Nickname = registerModel.Username,
                        IsBot = registerModel.IsBot
                    };

                    user.CreationDate = DateTime.Now;
                    var identityResult = await _userManager.CreateAsync(user, registerModel.Password);
                    if (identityResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        var accessToken = _tokenService.GenerateAccessToken(user);

                        // Syncing the Challenge DB User table
                        await SyncChallengeUserTable(user, accessToken);

                        // Syncing the Loyalty DB Loyalty table
                        await SyncLoyaltyTable(true, user, accessToken);

                        // Syncing the Loyalty DB User table
                        await SyncLoyaltyUserTable(true, user, accessToken);

                        // Syncing the Game DB User table
                        await SyncGameUserTable(user, accessToken);

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
                    return BadRequest(_localizer.GetString("InvalidTokenFacebook"));
                }

                // 4. ready to create the local user account (if necessary) and jwt
                var user = GetUserByExternalId(facebookUser.Id, ExternalProviderTyper.Facebook);
                var isCreate = user == null;

                var refreshToken = _tokenService.GenerateRefreshToken();

                var appUser = new ApplicationUser(facebookUser, refreshToken);

                if (isCreate)
                {
                    if (!IsEmailUnique(appUser.Email))
                        return BadRequest(_localizer.GetString("EmailIsInUse"));

                    appUser.CreationDate = DateTime.Now;
                    var result = await _userManager.CreateAsync(appUser, _randomPasswordHelper.GenerateRandomPassword());

                    if (!result.Succeeded)
                    {
                        return BadRequest(_localizer.GetString("LoginFailed"));
                    }
                }
                else
                {
                    user.RefreshToken = refreshToken;
                    user.PictureUrl = ImageHelper.SaveImageUrl(facebookUser.Picture.Data.Url, ImageFormat.Png);
                    user.LargePictureUrl = ImageHelper.SaveImageUrl(facebookUser.LargePictureUrl, ImageFormat.Png);

                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        return BadRequest(_localizer.GetString("LoginFailed"));
                    }
                }

                // generate the jwt for the local user...
                var localUser = GetUserByExternalId(facebookUser.Id, ExternalProviderTyper.Facebook);

                if (localUser == null)
                {
                    return BadRequest(_localizer.GetString("LoginFailed"));
                }

                var accessToken = _tokenService.GenerateAccessToken(localUser);                
                
                // Syncing the Challenge DB User table
                await SyncChallengeUserTable(localUser, accessToken);

                // Syncing the Loyalty DB Loyalty table
                await SyncLoyaltyTable(isCreate, localUser, accessToken);

                // Syncing the Loyalty DB User table
                await SyncLoyaltyUserTable(isCreate, localUser, accessToken);

                // Syncing the Game DB User table
                await SyncGameUserTable(localUser, accessToken);

                return Ok(new TokenModel(accessToken, refreshToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _localizer.GetString("LoginFailed"));
                return BadRequest(_localizer.GetString("LoginFailed"));
            }
            
        }

        private async Task SyncGameUserTable(ApplicationUser localUser, string accessToken)
        {
            // Syncing the Challenge DB User table
            var createGameUserViewModel = _mapper.Map<ApplicationUser, CreateOrUpdateGameUserViewModel>(localUser);
            createGameUserViewModel.UserId = localUser.Id;

            await _gameUserSyncService.SyncGameUserTable(createGameUserViewModel, accessToken);
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
            CreateOrUpdateLoyaltyViewModel createOrUpdateLoyaltyViewModel;
            if (isCreate)
            {
                createOrUpdateLoyaltyViewModel = new CreateOrUpdateLoyaltyViewModel()
                {
                    UserId = localUser.Id,
                    Credit = 50,
                    Score = 0,
                    Win = 0
                };
                await _loyaltySyncService.SyncLoyaltyTable(createOrUpdateLoyaltyViewModel, accessToken);
            }
        }

        private async Task SyncLoyaltyUserTable(bool isCreate, ApplicationUser localUser, string accessToken)
        {
            var createLoyaltyUserViewModel = _mapper.Map<ApplicationUser, CreateOrUpdateLoyaltyUserViewModel>(localUser);
            createLoyaltyUserViewModel.IsBotActivated = localUser.IsBot;

            createLoyaltyUserViewModel.UserId = localUser.Id;

            await _loyaltySyncService.SyncLoyaltyUserTable(createLoyaltyUserViewModel, accessToken);
        }

        // POST api/externalauth/google
        [HttpPost]
        [Route("anon/google")]
        public async Task<IActionResult> Google([FromBody]GoogleAuthViewModel model)
        {
            try
            {
                var googleUser = await _googleService.GetAccountAsync(model.AccessToken);

                if (string.IsNullOrEmpty(googleUser.Sub))
                {
                    return BadRequest(_localizer.GetString("InvalidTokenFacebook"));
                }

                // 4. ready to create the local user account (if necessary) and jwt
                var user = GetUserByExternalId(googleUser.Sub, ExternalProviderTyper.Google);
                var isCreate = user == null;
                var refreshToken = _tokenService.GenerateRefreshToken();
                var projectPath = _hostingEnvironment.ContentRootPath;
                
                if (user == null)
                {
                    var pictureUrl = ImageHelper.SaveImageUrl(googleUser.Picture, ImageFormat.Jpeg);

                    var appUser = new ApplicationUser
                    {
                        FirstName = googleUser.GivenName,
                        LastName = googleUser.FamilyName,
                        GoogleId = googleUser.Sub,
                        Email = googleUser.Email,
                        Nickname = Regex.Replace(googleUser.Name, @"[^\w]", "").ToLower(),
                        UserName = Guid.NewGuid().ToString(),
                        PictureUrl = pictureUrl,
                        LargePictureUrl = pictureUrl,
                        Birthday = googleUser.Birthday != null ? DateTime.ParseExact(googleUser.Birthday, "MM/dd/yyyy", CultureInfo.InvariantCulture) : DateTime.MinValue,
                        RefreshToken = refreshToken
                    };

                    if (!IsEmailUnique(appUser.Email))
                        return BadRequest(_localizer.GetString("EmailIsInUse"));

                    appUser.CreationDate = DateTime.Now;
                    var result = await _userManager.CreateAsync(appUser, _randomPasswordHelper.GenerateRandomPassword());

                    if (!result.Succeeded)
                    {
                        return BadRequest(_localizer.GetString("LoginFailed"));
                    }
                }

                // generate the jwt for the local user...
                var localUser = GetUserByExternalId(googleUser.Sub, ExternalProviderTyper.Google);

                if (localUser == null)
                {
                    return BadRequest(_localizer.GetString("LoginFailed"));
                }

                var accessToken = _tokenService.GenerateAccessToken(localUser);

                // Syncing the Challenge DB User table
                await SyncChallengeUserTable(localUser, accessToken);

                // Syncing the Loyalty DB Loyalty table
                await SyncLoyaltyTable(isCreate, localUser, accessToken);

                // Syncing the Game DB User table
                await SyncGameUserTable(localUser, accessToken);

                return Ok(new TokenModel(accessToken, refreshToken));
            }
            catch (Exception)
            {
                return BadRequest(_localizer.GetString("LoginFailed"));
            }
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
            var user = await _userManager.FindByNameAsync(
                User.Identity.Name ??
                User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault());

            return user;
        }

        [HttpGet]
        [Route("images")]
        public async Task<IActionResult> Images()
        {
            var user = await GetCurrentUserAsync();

            var images = _imageService.GetImages(user.Id);
            var imageListModel = _mapper.Map<List<ImageListModel>>(images);

            return Ok(imageListModel);
        }

        [HttpPost]
        [Route("AddImageByUrl")]
        public async Task<IActionResult> AddImageByUrl([FromBody]AddImageByUrlViewModel model)
        {
            var user = await GetCurrentUserAsync();

            var imagePath = ImageHelper.SaveImageUrl(model.ImagePath, ImageFormat.Png, model.Title);

            var imageParams = _mapper.Map<ImageParams>(model);
            imageParams.UserId = user.Id;
            imageParams.ImagePath = imagePath;

            var image = _imageService.CreateOrUpdate(imageParams);
            var imageModel = _mapper.Map<ImageListModel>(image);

            return Ok(imageModel);
        }

        [HttpPost]
        [Route("AddImage")]
        public async Task<IActionResult> AddImage([FromBody]AddImageViewModel model)
        {
            var user = await GetCurrentUserAsync();

            var imagePath = ImageHelper.SaveImage(model.ImageData, model.Title);
            var imageParams = new ImageParams(user.Id, model.Title, model.Description, imagePath);

            var image = _imageService.CreateOrUpdate(imageParams);
            var imageModel = _mapper.Map<ImageListModel>(image);

            return Ok(imageModel);
        }
    }
}