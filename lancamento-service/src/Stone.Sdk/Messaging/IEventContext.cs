using MediatR;

namespace Stone.Sdk.Messaging
{
    public interface IEventContext : IMessageContext, INotification
    {
        
    }
}