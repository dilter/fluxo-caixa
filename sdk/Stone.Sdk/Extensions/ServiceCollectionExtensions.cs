using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using RabbitMQ.Client;
using Stone.Sdk.Application;
using Stone.Sdk.Messaging;
using Stone.Sdk.Persistence;

namespace Stone.Sdk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddElastisearch(this IServiceCollection serviceCollection,
            IConfiguration configuration, Type[] indicesTypes)
        {
            var elasticsearchUri = configuration.GetConnectionString("ElasticsearchConnection");
            serviceCollection.AddScoped(e =>
            {
                var settings = new ConnectionSettings(new Uri(elasticsearchUri));                
                var client = new ElasticClient(settings);                
                foreach (var type in indicesTypes)
                {
                    settings.DefaultMappingFor(type, x =>
                    {
                        x.IndexName(type.Name);
                        x.TypeName(type.Name);
                        return x;
                    });
                }
                return client;
            });
            serviceCollection.AddScoped<IIndexer, ElasticsearchIndexer>();
            return serviceCollection;
        }
        
        public static void AddMessageBroker(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MessageBrokerConnection");
            serviceCollection.AddSingleton(c =>
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(connectionString),
                };
                return factory.CreateConnection();
            });
            serviceCollection.AddScoped<AmqpClient>();
            serviceCollection.AddScoped<ICommandBus, CommandBus>();
            serviceCollection.AddScoped<IEventBus, EventBus>();
        }
    }
}