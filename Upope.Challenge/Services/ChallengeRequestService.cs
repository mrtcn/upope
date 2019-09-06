using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Upope.Challenge.CustomExceptions;
using Upope.Challenge.Data.Entities;
using Upope.Challenge.EntityParams;
using Upope.Challenge.GlobalSettings;
using Upope.Challenge.Hubs;
using Upope.Challenge.Services.Interfaces;
using Upope.Challenge.Services.Models;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Handler;
using Upope.ServiceBase.Models;
using Upope.ServiceBase.Services.Interfaces;

namespace Upope.Challenge.Services
{
    public class ChallengeRequestService : EntityServiceBase<ChallengeRequest>, IChallengeRequestService
    {
        private readonly IUserService _userService;
        private readonly IChallengeService _challengeService;
        private readonly IGeoLocationService _geoLocationService;
        private readonly IHubContext<ChallengeHub> _hubContext;
        private readonly IHttpHandler _httpHandler;
        private readonly IMapper _mapper;

        public ChallengeRequestService(
            ApplicationDbContext applicationDbContext,
            IUserService userService,
            IChallengeService challengeService,
            IGeoLocationService geoLocationService,
            IMapper mapper, 
            IHttpHandler httpHandler,
            IHubContext<ChallengeHub> hubContext) : base(applicationDbContext, mapper)
        {
            _userService = userService;
            _challengeService = challengeService;
            _geoLocationService = geoLocationService;
            _hubContext = hubContext;
            _mapper = mapper;
            _httpHandler = httpHandler;
        }

        protected override void OnSaveChanges(IEntityParams entityParams, ChallengeRequest entity)
        {
            base.OnSaveChanges(entityParams, entity);

            var challengeRequestParams = entityParams as ChallengeRequestParams;

            // If the Challenger tries to accept a game which its user is already in another game
            if (challengeRequestParams.Id == 0 
                && challengeRequestParams.ChallengeRequestStatus == Enums.ChallengeRequestStatus.Accepted 
                && IsUserInTheGame(challengeRequestParams.ChallengerId))
            {
                throw new UserNotAvailableException();
            }
        }

        protected override async void OnSaveChangedAsync(IEntityParams entityParams, ChallengeRequest entity)
        {
            base.OnSaveChangedAsync(entityParams, entity);
            var challengeRequestParams = entityParams as ChallengeRequestParams;
            var accessToken = challengeRequestParams.AccessToken;

            entity.Challenger = Entities
                .Include(x => x.Challenger)
                .Where(x => x.ChallengerId == entity.ChallengerId)
                .Select(x => x.Challenger).FirstOrDefault();

            entity.Challenge = _challengeService.Get(entity.ChallengeId);

            challengeRequestParams = _mapper.Map<ChallengeRequest, ChallengeRequestParams>(entity);
            var rnd = new Random();

            if (challengeRequestParams.ChallengeRequestStatus == Enums.ChallengeRequestStatus.Accepted)
            {
                var userIds = SetChallengeRequestsToMissed(challengeRequestParams.ChallengeId, challengeRequestParams.Id);

                await _hubContext.Clients.Users(userIds)
                .SendAsync("ChallengeRequestMissed", challengeRequestParams.Id);
                
                await _hubContext.Clients.User(challengeRequestParams.ChallengeOwnerId)
                .SendAsync("ChallengeRequestAccepted", JsonConvert.SerializeObject(new ChallengeRequestModel()
                {
                    ChallengeRequestId = challengeRequestParams.Id,
                    Point = entity.Challenge.RewardPoint,
                    Range = rnd.Next(1, 150).ToString() + " Meter",
                    FirstName = entity.Challenger.Nickname,
                    UserImagePath = entity.Challenger.PictureUrl
                }));


            }

            if (entity.ChallengeRequestStatus == Enums.ChallengeRequestStatus.Rejected)
            {
                var challengeRequestModel = GetChallengeRequest(challengeRequestParams.Id);
            }
        }

        public List<ChallengeRequestModel> ChallengeRequests(string userId)
        {
            Random rnd = new Random();

            var challengeRequestsParamList = Entities
                .Include(x => x.Challenge)
                .Include(x => x.Challenger)
                .Where(x => x.ChallengerId == userId && x.ChallengeRequestStatus == Enums.ChallengeRequestStatus.Waiting)
                .Select(x => new ChallengeRequestModel()
                {
                    ChallengeRequestId = x.Id,
                    Point = x.Challenge.RewardPoint,
                    Range =  rnd.Next(1, 150).ToString() + " Meter",
                    FirstName = x.Challenger.Nickname,
                    UserImagePath = x.Challenger.PictureUrl
                })
                .ToList();
            return challengeRequestsParamList;

        }

        public async Task RejectAcceptChallenge(RejectAcceptChallengeModel model)
        {
            var challengeRequestList = Entities
                .Include(x => x.Challenge)
                .Include(x => x.Challenger)
                .Where(x => x.ChallengeId == model.ChallengeId && x.ChallengeOwnerId == model.UserId).Take(8).ToList();

            var challengeRequestAmount = challengeRequestList.Count() - 1;

            int index = 0;
            var rnd = new Random();

            var previousChallengeRequest = new ChallengeRequest();

            foreach (var challengeRequest in challengeRequestList)
            {
                if(index != challengeRequestAmount)
                {
                    await _hubContext.Clients.User(challengeRequest.ChallengeOwnerId)
                    //await _hubContext.Clients.All
                        .SendAsync("ChallengeRequestRejected", JsonConvert.SerializeObject(new ChallengeRequestModel()
                        {
                            ChallengeRequestId = challengeRequest.Id,
                            Point = challengeRequest.Challenge.RewardPoint,
                            Range = rnd.Next(1, 150).ToString() + " Meter",
                            FirstName = challengeRequest.Challenger.Nickname,
                            UserImagePath = challengeRequest.Challenger.PictureUrl
                        }));

                    Thread.Sleep(1000);

                    if(previousChallengeRequest.Id > 0)
                    {
                        var challengeRequestParams = _mapper.Map<ChallengeRequestParams>(previousChallengeRequest);
                        challengeRequestParams.ChallengeRequestStatus = Enums.ChallengeRequestStatus.Rejected;
                        CreateOrUpdate(challengeRequestParams);
                    }

                    var challenge = _challengeService.CreateOrUpdate(new ChallengeParams(Status.Active, challengeRequest.ChallengerId, 20));
                    previousChallengeRequest = CreateOrUpdate(new ChallengeRequestParams(Status.Active, challengeRequest.ChallengerId, model.UserId, challenge.Id, Enums.ChallengeRequestStatus.Waiting));
                }
                else
                {
                    await _hubContext.Clients.User(challengeRequest.ChallengeOwnerId)
                    //await _hubContext.Clients.All
                        .SendAsync("ChallengeRequestAccepted", JsonConvert.SerializeObject(new ChallengeRequestModel()
                        {
                            ChallengeRequestId = challengeRequest.Id,
                            Point = challengeRequest.Challenge.RewardPoint,
                            Range = rnd.Next(1, 150).ToString() + " Meter",
                            FirstName = challengeRequest.Challenger.Nickname,
                            UserImagePath = challengeRequest.Challenger.PictureUrl
                        }));
                }

                index++;

                Thread.Sleep(3000);
            }

            var users =_userService.Entities.Where(x => x.UserId != model.UserId).ToList();

            foreach (var user in users)
            {
                var challenge = _challengeService.CreateOrUpdate(new ChallengeParams(Status.Active, user.UserId, 40));
                await CreateChallengeRequestForUser(new CreateChallengeRequestForUserModel(model.AccessToken, challenge.Id, user.UserId, model.UserId, challenge.RewardPoint));

                Thread.Sleep(3000);
            }

        }

        public async Task<IReadOnlyList<string>> CreateChallengeRequests(CreateChallengeRequestModel model)
        {
            var userIds = await GetChallengerIds(
                new GetChallengerIdsModel(
                    model.AccessToken, 
                    AppSettingsProvider.LoyaltyBaseUrl, 
                    AppSettingsProvider.SufficientPointsUrl + "/" + model.Points));

            var filteredUserId = ExcludeOutOfRangeUsers(model.Range, model.ChallengeOwnerId, userIds);
            filteredUserId = ApplyGenderFilter(model.Gender, model.ChallengeOwnerId, filteredUserId);

            foreach (var userId in filteredUserId)
            {
                var challengeRequestParams = new ChallengeRequestParams(Status.Active, model.ChallengeOwnerId, userId, model.ChallengeId, Enums.ChallengeRequestStatus.Waiting);
                CreateOrUpdate(challengeRequestParams);

                await _hubContext.Clients.User(userId)
                    .SendAsync("ChallengeRequestReceived", JsonConvert.SerializeObject(GetChallengeRequest(challengeRequestParams.Id)));
            }

            return filteredUserId;
        }

        private async Task CreateChallengeRequestForUser(CreateChallengeRequestForUserModel model)
        {
            var challengeRequestParams = new ChallengeRequestParams(Status.Active, model.ChallengeOwnerId, model.ChallengerId, model.ChallengeId, Enums.ChallengeRequestStatus.Waiting);
            CreateOrUpdate(challengeRequestParams);

            await _hubContext.Clients.Users(model.ChallengerId)
                .SendAsync("ChallengeRequestReceived", JsonConvert.SerializeObject(GetChallengeRequest(challengeRequestParams.Id)));
        }

        //TODO: (Murat) Performance issue to solve
        private IReadOnlyList<string> ApplyGenderFilter(Gender gender, string actualUserId, IReadOnlyList<string> destinationUserIds)
        {
            if (destinationUserIds == null || destinationUserIds.Count == 0)
                return null;

            if (gender == Gender.Unknown)
                return destinationUserIds;

            var userIds = destinationUserIds.ToList();

            foreach (var userId in userIds.ToList())
            {
                var destinationUser = _userService.GetUserByUserId(userId);

                if (destinationUser.Gender != gender)
                    userIds.Remove(userId);
            }

            return userIds;
        }

        //TODO: (Murat) Performance issue to solve
        private IReadOnlyList<string> ExcludeOutOfRangeUsers(int range, string actualUserId, IReadOnlyList<string> destinationUserIds)
        {
            if (destinationUserIds == null || destinationUserIds.Count == 0)
                return null;

            if (range == 0)
                return destinationUserIds;

            var userIds = destinationUserIds.ToList();
            var actualUser = _userService.GetUserByUserId(actualUserId);
            foreach(var userId in userIds.ToList())
            {
                var destinationUser = _userService.GetUserByUserId(userId);
                var distance = _geoLocationService.GetDistance(
                    new CoordinateModel(actualUser.Latitude, actualUser.Longitude),
                    new CoordinateModel(destinationUser.Latitude, destinationUser.Longitude));

                if (distance > range)
                    userIds.Remove(userId);
            }

            return userIds;
        }

        private bool IsUserInTheGame(string userId)
        {
            return Entities
                .Any(x => (x.Challenge.ChallengerId == userId) || (x.Challenge.ChallengeOwnerId == userId) 
                && x.ChallengeRequestStatus == Enums.ChallengeRequestStatus.Accepted);
        }

        private List<string> NotAvailableUserIds(List<string> userIds)
        {
            return Entities.Include(x => x.Challenge)
                .Where(x => (userIds.Contains(x.Challenge.ChallengerId) || userIds.Contains(x.Challenge.ChallengeOwnerId)) && x.ChallengeRequestStatus == Enums.ChallengeRequestStatus.Accepted)
                .Select(x => x.ChallengerId)
                .ToList();
        }

        private async Task<IReadOnlyList<string>> GetChallengerIds(GetChallengerIdsModel model)
        {
            if (string.IsNullOrEmpty(model.BaseUrl))
                model.BaseUrl = AppSettingsProvider.LoyaltyBaseUrl;

            if (string.IsNullOrEmpty(model.Api))
                model.Api = AppSettingsProvider.SufficientPointsUrl;

            var userIds = await _httpHandler.AuthGetAsync<IReadOnlyList<string>>(model.AccessToken, model.BaseUrl, model.Api);

            if(userIds.Any())
                userIds.Except(NotAvailableUserIds(userIds.ToList()));

            return userIds;
        }

        private List<string> SetChallengeRequestsToMissed(int challengeId, int challengeRequestId)
        {
            var challengeRequestIds = Entities.Where(x => x.ChallengeId == challengeId && x.Id != challengeRequestId).Select(x => x.Id).ToList();

            var userIds = new List<string>();
            ChallengeRequestParams challengeRequestParams = new ChallengeRequestParams();

            foreach (var id in challengeRequestIds)
            {
                var challengeRequest = Get(id);
                challengeRequestParams = _mapper.Map<ChallengeRequest, ChallengeRequestParams>(challengeRequest);
                challengeRequestParams.ChallengeRequestStatus = Enums.ChallengeRequestStatus.Missed;
                CreateOrUpdate(challengeRequestParams);

                userIds.Add(challengeRequest.ChallengerId);
            }

            return userIds;            
        }

        private ChallengeRequestModel GetChallengeRequest(int challengeRequestId)
        {
            var rnd = new Random();

            var challengeRequestModel = Entities
                .Include(x => x.Challenge)
                .Include(x => x.ChallengOwner)
                .Where(x => x.Id == challengeRequestId)
                .ToList()
                .Select(x => new ChallengeRequestModel()
                {
                    ChallengeRequestId = x.Id,
                    Point = x.Challenge.RewardPoint,
                    //TODO: Range units should be localized
                    Range = _geoLocationService.GetDistance(
                        new CoordinateModel(x.Challenger.Latitude, x.Challenger.Longitude), 
                        new CoordinateModel(x.ChallengOwner.Latitude, x.ChallengOwner.Longitude)).ToString() + " Meter",
                    UserId = x.ChallengeOwnerId,
                    FirstName = x.ChallengOwner.Nickname,
                    UserImagePath = x.ChallengOwner.PictureUrl
                }).FirstOrDefault();

            return challengeRequestModel;
        }
    }
}
