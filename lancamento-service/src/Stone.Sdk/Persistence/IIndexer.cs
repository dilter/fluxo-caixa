using System.Threading.Tasks;

namespace Stone.Sdk.Persistence
{
    public interface IIndexer
    {
        Task IndexAsync<TIndex>(TIndex document) where TIndex : class;
    }
}