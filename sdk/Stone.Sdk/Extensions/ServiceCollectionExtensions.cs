using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Stone.Sdk.Application;
using Stone.Sdk.Messaging;

namespace Stone.Sdk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MessageBrokerConnection");
            serviceCollection.AddSingleton<IConnection>(c =>
            {
                var factory = new ConnectionFactory()
                {
                    Uri = new Uri(connectionString),
                };
                return factory.CreateConnection();
            });
            serviceCollection.AddScoped<AmqpClient>();
            serviceCollection.AddScoped<ICommandBus, CommandBus>();
            serviceCollection.AddScoped<IEventBus, EventBus>();
            return serviceCollection;
        }
    }
}