using System.Collections.Generic;
using Upope.Game.Data.Entities;
using Upope.Game.EntityParams;
using Upope.ServiceBase;

namespace Upope.Game.Services.Interfaces
{
    public interface IRoundAnswerService : IEntityServiceBase<RoundAnswer>
    {
        int RoundAnswerCount(int gameRoundId, string userId);
        List<RoundAnswerParams> RoundAnswers(int gameRoundId);
    }
}
