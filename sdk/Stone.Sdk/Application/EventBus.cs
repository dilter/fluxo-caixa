using System.Threading.Tasks;
using RabbitMQ.Client.Framing;
using Stone.Sdk.Messaging;

namespace Stone.Sdk.Application
{
    public class EventBus : Bus, IEventBus
    {                           
        public EventBus(AmqpClient amqpClient) 
            : base(amqpClient) { }

        private static EventContext<TEvent> CreateEventContext<TEvent>(TEvent @event, IMessageContext context = null)
            where TEvent : IEvent
        {
            return new EventContext<TEvent>(@event, id: context?.Id, requestId: context?.RequestId,
                correlationId: context?.CorrelationId) {Metadata = context?.Metadata};
        }

        private static string GetExchangeName(IEvent @event)
        {
            var queuePrefix = @event.GetType().FullName.Split('.').GetValue(1).ToString();
            return $"{queuePrefix}.{@event.GetType().Name}";
        }
        
        public async Task PublishAsync<TEvent>(TEvent @event, IMessageContext context = null) 
            where TEvent : IEvent
        {
            var eventContext = CreateEventContext(@event, context);            
            var exchangeName = GetExchangeName(@event);            
            await _amqpClient.SendMessageAsync(
                exchangeName: exchangeName,
                exchangeType: string.IsNullOrEmpty(context?.ReplyEvent) ? "fanout" : "direct",
                message: eventContext.Event, 
                routingKey: context?.ReplyEvent,
                basicProperties: new BasicProperties
                {
                    CorrelationId = eventContext.Id.ToString("D"),
                    ReplyTo = eventContext.RequestId.ToString("D"),
                    Headers = eventContext.Metadata,                    
                }
            );                        
        }
    }
}