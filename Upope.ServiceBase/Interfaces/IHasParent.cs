
namespace Upope.ServiceBase.Interfaces
{
    public interface IHasParent<T>
    {
        T BaseEntity { get; set; }
        int BaseEntityId { get; set; }
    }
}
