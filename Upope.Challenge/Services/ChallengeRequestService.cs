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

namespace Upope.Challenge.Services
{
    public class ChallengeRequestService : EntityServiceBase<ChallengeRequest>, IChallengeRequestService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUserService _userService;
        private readonly IChallengeService _challengeService;
        private readonly IHubContext<ChallengeHubs> _hubContext;
        private readonly IHttpHandler _httpHandler;
        private readonly IMapper _mapper;

        public ChallengeRequestService(
            ApplicationDbContext applicationDbContext,
            IUserService userService,
            IChallengeService challengeService,
            IMapper mapper, 
            IHttpHandler httpHandler,
            IHubContext<ChallengeHubs> hubContext) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _userService = userService;
            _challengeService = challengeService;
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

            entity.Challenger = Entities
                .Include(x => x.Challenger)
                .Where(x => x.ChallengerId == entity.ChallengerId)
                .Select(x => x.Challenger).FirstOrDefault();

            var challengeRequestParams = _mapper.Map<ChallengeRequest, ChallengeRequestParams>(entity);
            var rnd = new Random();

            if (challengeRequestParams.ChallengeRequestStatus == Enums.ChallengeRequestStatus.Accepted)
            {
                var userIds = SetChallengeRequestsToMissed(challengeRequestParams.ChallengeId, challengeRequestParams.Id);

                await _hubContext.Clients.Users(userIds)
                //await _hubContext.Clients.All
                .SendAsync("ChallengeRequestMissed", challengeRequestParams.Id);

                await _hubContext.Clients.User(challengeRequestParams.ChallengeOwnerId)
                //await _hubContext.Clients.All
                .SendAsync("ChallengeRequestAccepted", JsonConvert.SerializeObject(new ChallengeRequestModel()
                {
                    Id = challengeRequestParams.Id,
                    Point = entity.Challenge.RewardPoint,
                    Range = rnd.Next(1, 150).ToString() + " Meter",
                    UserName = entity.Challenger.Nickname,
                    UserImagePath = entity.Challenger.PictureUrl
                }));
            }

            if (entity.ChallengeRequestStatus == Enums.ChallengeRequestStatus.Rejected)
            {
                var challengeRequestModel = GetChallengeRequest(challengeRequestParams.Id);
                
                await _hubContext.Clients.User(challengeRequestParams.ChallengeOwnerId)
                //await _hubContext.Clients.All
                .SendAsync("ChallengeRequestRejected", JsonConvert.SerializeObject(new ChallengeRequestModel() {
                    Id = challengeRequestParams.Id,
                    Point = challengeRequestModel.Point,
                    Range = rnd.Next(1, 150).ToString() + " Meter",
                    UserName = entity.Challenger.Nickname,
                    UserImagePath = entity.Challenger.PictureUrl
                }));
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
                    Id = x.Id,
                    Point = x.Challenge.RewardPoint,
                    Range =  rnd.Next(1, 150).ToString() + " Meter",
                    UserName = x.Challenger.Nickname,
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
                .Where(x => x.ChallengeId == model.ChallengeId).ToList();

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
                            Id = challengeRequest.Id,
                            Point = challengeRequest.Challenge.RewardPoint,
                            Range = rnd.Next(1, 150).ToString() + " Meter",
                            UserName = challengeRequest.Challenger.Nickname,
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
                            Id = challengeRequest.Id,
                            Point = challengeRequest.Challenge.RewardPoint,
                            Range = rnd.Next(1, 150).ToString() + " Meter",
                            UserName = challengeRequest.Challenger.Nickname,
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

        private async Task CreateChallengeRequestForUser(CreateChallengeRequestForUserModel model)
        {
            var challengeRequestParams = new ChallengeRequestParams(Status.Active, model.ChallengeOwnerId, model.ChallengerId, model.ChallengeId, Enums.ChallengeRequestStatus.Waiting);
            CreateOrUpdate(challengeRequestParams);

            await _hubContext.Clients.Users(model.ChallengerId)
            //await _hubContext.Clients.All
                .SendAsync("ChallengeRequestReceived", JsonConvert.SerializeObject(GetChallengeRequest(challengeRequestParams.Id)));
        }

        public async Task<IReadOnlyList<string>> CreateChallengeRequests(CreateChallengeRequestModel model)
        {
            var points = JsonConvert.SerializeObject(new PointsModel(model.Points));
            var userIds = await GetChallengerIds(new HttpCallModel(model.AccessToken, AppSettingsProvider.LoyaltyBaseUrl, AppSettingsProvider.SufficientPointsUrl, points));
            var challengeRequestParams = new ChallengeRequestParams();

            foreach (var userId in userIds)
            {
                challengeRequestParams = new ChallengeRequestParams(Status.Active, model.ChallengeOwnerId, userId, model.ChallengeId, Enums.ChallengeRequestStatus.Waiting);
                CreateOrUpdate(challengeRequestParams);
            }

            await _hubContext.Clients.Users(userIds)
            //await _hubContext.Clients.All
                .SendAsync("ChallengeRequestReceived", JsonConvert.SerializeObject(GetChallengeRequest(challengeRequestParams.Id)));

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

        private async Task<IReadOnlyList<string>> GetChallengerIds(HttpCallModel model)
        {
            if (string.IsNullOrEmpty(model.BaseUrl))
                model.BaseUrl = AppSettingsProvider.LoyaltyBaseUrl;

            if (string.IsNullOrEmpty(model.Api))
                model.Api = AppSettingsProvider.SufficientPointsUrl;

            var userIds = await _httpHandler.AuthPostAsync<IReadOnlyList<string>>(model.AccessToken, model.BaseUrl, model.Api, model.MessageBody);

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
                .Where(x => x.Id == challengeRequestId)
                .Select(x => new ChallengeRequestModel()
                {
                    Id = x.Id,
                    Point = x.Challenge.RewardPoint,
                    Range = rnd.Next(1, 150).ToString() + " Meter",
                    UserName = x.Challenger.Nickname,
                    UserImagePath = x.Challenger.PictureUrl
                }).FirstOrDefault();

            return challengeRequestModel;
        }
    }
}
