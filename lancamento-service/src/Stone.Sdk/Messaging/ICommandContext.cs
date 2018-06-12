using MediatR;

namespace Stone.Sdk.Messaging
{
    public interface ICommandContext : IMessageContext, IRequest
    {
        
    }
}