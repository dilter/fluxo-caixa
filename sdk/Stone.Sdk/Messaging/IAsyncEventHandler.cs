using MediatR;

namespace Stone.Sdk.Messaging
{
    public interface IAsyncEventHandler<TEvent> : IAsyncNotificationHandler<EventContext<TEvent>> 
        where TEvent : IEvent
    {
        
    }

}