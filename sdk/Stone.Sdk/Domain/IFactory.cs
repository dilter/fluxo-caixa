using Stone.Sdk.Persistence;

namespace Stone.Sdk.Domain
{
    public interface IFactory<out TEntity>
        where TEntity : Entity
    {
        
    }
}