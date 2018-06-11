using System.Threading.Tasks;
using Nest;
using Stone.Sdk.Persistence;

namespace Stone.Sdk.Application
{
    public class ElasticsearchIndexer : IIndexer
    {
        private readonly ElasticClient _elasticClient;
        public ElasticsearchIndexer(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task IndexAsync<TIndex>(TIndex document) where TIndex : class
        {            
            await _elasticClient.IndexDocumentAsync(document);
        }
    }
}