using MediatR;

namespace Stone.Sdk.Messaging
{
    public interface IAsyncCommandHandler<TCommand> : IAsyncRequestHandler<CommandContext<TCommand>> 
        where TCommand : ICommand
    {
        
    }
}