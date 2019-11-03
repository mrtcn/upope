using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upope.Challenge.GlobalSettings;
using Upope.Challenge.Services.Interfaces;
using Upope.Challenge.ViewModels;
using Upope.ServiceBase.Handler;

namespace Upope.Challenge.Services
{
    public class BotService : IBotService
    {
        private readonly IMapper _mapper;
        private readonly IHttpHandler _httpHandler;
        public BotService(
            ApplicationDbContext applicationDbContext,
            IHttpHandler httpHandler,
            IMapper mapper)
        {
            _mapper = mapper;
            _httpHandler = httpHandler;
        }


        public Task AcceptChallengeRequest()
        {
            throw new NotImplementedException();
        }

        public Task SendAnswer()
        {
            throw new NotImplementedException();
        }

        public Task SendBluff()
        {
            throw new NotImplementedException();
        }

        public Task SendSuperBluff()
        {
            throw new NotImplementedException();
        }

        public async Task SendUpdateChallengeRequest(string accessToken, int challengeRequestId, UpdateChallengeInputViewModel model)
        {
            var baseUrl = AppSettingsProvider.ChallengeBaseUrl;

            var api = AppSettingsProvider.UpdateChallenge.Replace("{challengeRequestId}", challengeRequestId.ToString());

            var messageBody = JsonConvert.SerializeObject(model);
            await _httpHandler.AuthPutAsync(accessToken, baseUrl, api, messageBody);
        }
    }
}
