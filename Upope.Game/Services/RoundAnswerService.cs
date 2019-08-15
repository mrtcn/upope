using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.Game.Services.Interfaces;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;

namespace Upope.Game.Services
{    
    public class RoundAnswerService : EntityServiceBase<RoundAnswer>, IRoundAnswerService
    {
        private readonly IMapper _mapper;

        public RoundAnswerService(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper) : base(applicationDbContext, mapper)
        {
            _mapper = mapper;
        }

        public int RoundAnswerCount(int gameRoundId, string userId)
        {
            var roundAnswerCount = Entities.Where(x => x.Status == Status.Active && x.GameRoundId == gameRoundId && x.UserId == userId).Count();

            return roundAnswerCount;
        }

        public List<RoundAnswerParams> RoundAnswers(int gameRoundId)
        {
            var roundAnswers = Entities.Where(x => x.Status == Status.Active && x.GameRoundId == gameRoundId).ToList();
            var roundAnswerParamsList = _mapper.Map< List<RoundAnswer>, List <RoundAnswerParams>>(roundAnswers);

            return roundAnswerParamsList;
        }
    }
}
