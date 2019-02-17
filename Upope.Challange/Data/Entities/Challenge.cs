using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;

namespace Upope.Challange.Data.Entities
{
    public interface IChallenge : IEntity, IHasStatus
    {
    }
    public class Challenge: IChallenge
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}
