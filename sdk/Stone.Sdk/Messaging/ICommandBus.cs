using System;
using System.Threading.Tasks;

namespace Stone.Sdk.Messaging
{
    public interface ICommandBus
    {
        Task SendAsync<TCommand, TReplyEvent>(TCommand command, IMessageContext context = null)
            where TCommand : ICommand;
        
        Task SendAsync<TCommand>(TCommand command, IMessageContext context = null, Type replyEventType = null) 
            where TCommand : ICommand;
    }
}