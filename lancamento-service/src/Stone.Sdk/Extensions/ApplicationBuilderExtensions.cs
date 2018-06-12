using System;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Stone.Sdk.Messaging;

namespace Stone.Sdk.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSubcriberFor<TEvent, TBindEvent>(this IApplicationBuilder builder) 
            where TEvent : IEvent 
            where TBindEvent : IEvent
        {
            UseSubcriberFor<TEvent>(builder, typeof(TBindEvent));
        }

        private static string ExtractMessageBody(BasicDeliverEventArgs ea)
        {
            var body = ea.Body;
            return Encoding.UTF8.GetString(body);
        }
        
        public static void UseSubcriberFor<TEvent>(this IApplicationBuilder builder, Type typeBindEvent = null)
            where TEvent : IEvent
        {
            var hasTypeBindEvent = typeBindEvent != null;
            var routingKeys = hasTypeBindEvent ? new[] {typeBindEvent.FullName} : null;
            var connection = builder.ApplicationServices.GetService<IConnection>();
            var mediator = builder.ApplicationServices.GetService<IMediator>();
            var channel = connection.CreateModel();
            var exchangePrefix = typeof(TEvent).FullName.Split('.').GetValue(1);
            var exchangeName = $"{exchangePrefix}.{typeof(TEvent).Name}";
            var queueName = channel.QueueDeclare().QueueName;
            
            channel.ExchangeDeclare(exchange: exchangeName, type: routingKeys == null ? "fanout" : "direct");
            
            if (routingKeys == null)
            {
                channel.QueueBind(queue: queueName, 
                    exchange: exchangeName,
                    routingKey: ""
                );
            }
            else
            {
                foreach (var routingKey in routingKeys)
                {
                    channel.QueueBind(queue: queueName, 
                        exchange: exchangeName,
                        routingKey: routingKey
                    );    
                }
            }
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var typeArg = typeBindEvent == null ? typeof(TEvent) : typeBindEvent;                    
                    Type[] typeArgs = {typeArg, };
                    
                    var eventContextType = typeof(EventContext<>);
                    var constructed = eventContextType.MakeGenericType(typeArgs);
                    
                    var message = ExtractMessageBody(ea);
                    var @event = JsonConvert.DeserializeObject(message, typeArg);                    
                    var eventContext = Activator.CreateInstance(constructed, @event, Guid.Parse(ea.BasicProperties.CorrelationId), Guid.Parse(ea.BasicProperties.ReplyTo), Guid.Empty);
                    
                    ((IEventContext) eventContext).Metadata = ea.BasicProperties.Headers;
                    
                    await mediator.Publish((IEventContext)eventContext);
                }
                catch (Exception e)                
                {
                    Console.WriteLine(e.Message, e);
                }
            };
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }
        
        public static void UseCommandHandlerFor<TCommand>(this IApplicationBuilder builder)
            where TCommand : ICommand
        {
            var connection = builder.ApplicationServices.GetService<IConnection>();
            var mediator = builder.ApplicationServices.GetService<IMediator>();
            var channel = connection.CreateModel();
            var exchangePrefix = typeof(TCommand).FullName.Split('.').GetValue(1);
            var queueName = $"{exchangePrefix}.{typeof(TCommand).Name}";            
            
            channel.QueueDeclare(queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var command = JsonConvert.DeserializeObject(message, typeof(TCommand));
                
                    var commandContextType = typeof(CommandContext<>);
                    Type[] typeArgs = {typeof(TCommand)};
                    var constructed = commandContextType.MakeGenericType(typeArgs);
                    var commandContext = Activator.CreateInstance(constructed, (TCommand)command, ea.BasicProperties.Type, Guid.Parse(ea.BasicProperties.CorrelationId), Guid.Parse(ea.BasicProperties.ReplyTo), Guid.Empty);
                    ((ICommandContext)commandContext).Metadata = ea.BasicProperties.Headers;
                    await mediator.Send((ICommandContext)commandContext);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message, e);
                }
            };
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }
    }
}