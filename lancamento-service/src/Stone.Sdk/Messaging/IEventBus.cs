using System.Threading.Tasks;

namespace Stone.Sdk.Messaging
{
    public interface IEventBus
    {
        Task PublishAsync<TEvent>(TEvent @event, IMessageContext context = null) 
            where TEvent : IEvent;
    }
}