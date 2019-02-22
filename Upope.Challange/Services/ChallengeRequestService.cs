using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upope.Challange.Data.Entities;
using Upope.Challange.EntityParams;
using Upope.Challange.GlobalSettings;
using Upope.Challange.Hubs;
using Upope.Challange.Services.Interfaces;
using Upope.Challange.Services.Models;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Handler;

namespace Upope.Challange.Services
{
    public class ChallengeRequestService : EntityServiceBase<ChallengeRequest>, IChallengeRequestService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHubContext<ChallengeHubs> _hubContext;
        private readonly IHttpHandler _httpHandler;
        private readonly IMapper _mapper;

        public ChallengeRequestService(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper, 
            IHttpHandler httpHandler,
            IHubContext<ChallengeHubs> hubContext) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _hubContext = hubContext;
            _mapper = mapper;
            _httpHandler = httpHandler;
        }

        public List<ChallengeRequestParams> ChallengeRequests(string userId)
        {
            var challengeRequestsParamList = Entities
                .Where(x => x.ChallengerId == userId && x.ChallengeRequestStatus == Enums.ChallengeRequestStatus.Waiting)
                .Select(x => new ChallengeRequestParams(
                    x.Status, 
                    x.ChallengeOwnerId, 
                    x.ChallengerId, 
                    x.ChallengeId, 
                    x.ChallengeRequestStatus))
                .ToList();
            return challengeRequestsParamList;

        }

        public async Task<IReadOnlyList<string>> CreateChallengeRequests(CreateChallengeRequestModel model)
        {
            var points = JsonConvert.SerializeObject(new PointsModel(model.Points));
            var userIds = await GetChallengerIds(new HttpCallModel(model.AccessToken, AppSettingsProvider.LoyaltyBaseUrl, "api/Loyalty/GetChallengerIds", points));
            var challengeRequestParams = new ChallengeRequestParams();

            foreach (var userId in userIds)
            {
                challengeRequestParams = new ChallengeRequestParams(Status.Active, model.ChallengeOwnerId, userId, model.ChallengeId, Enums.ChallengeRequestStatus.Waiting);
                CreateOrUpdate(challengeRequestParams);
            }
            
            await _hubContext.Clients.Users(userIds)
                .SendAsync("SendChallenge", JsonConvert.SerializeObject(challengeRequestParams));

            return userIds;
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
                model.Api = "/api/Loyalty/SufficientPoints";

            var userIds = await _httpHandler.AuthPostAsync<IReadOnlyList<string>>(model.AccessToken, model.BaseUrl, model.Api, model.MessageBody);

            if(userIds.Any())
                userIds.Except(NotAvailableUserIds(userIds.ToList()));

            return userIds;
        }

        public async Task SetChallengeRequestsToMissed(int challengeId, int challengeRequestId)
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

            await _hubContext.Clients.Users(userIds)
                .SendAsync("RemoveChallengeRequest", JsonConvert.SerializeObject(challengeRequestParams));
        }
    }
}
