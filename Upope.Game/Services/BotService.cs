using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Upope.Game.GlobalSettings;
using Upope.Game.Services.Interfaces;
using Upope.Game.ViewModels;
using Upope.ServiceBase.Handler;

namespace Upope.Game.Services
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

        public async Task SendAnswer(string accessToken, SendChoiceViewModel model)
        {
            var baseUrl = AppSettingsProvider.GameBaseUrl;

            var api = AppSettingsProvider.SendChoice;

            var messageBody = JsonConvert.SerializeObject(model);
            await _httpHandler.AuthPostAsync(accessToken, baseUrl, api, messageBody);
        }

        public Task SendBluff()
        {
            throw new NotImplementedException();
        }

        public Task SendSuperBluff()
        {
            throw new NotImplementedException();
        }
    }
}
