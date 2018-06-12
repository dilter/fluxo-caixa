using System;
using System.Collections.Generic;

namespace Stone.Sdk.Messaging
{
    public class CommandContext<TCommand> :  ICommandContext
        where TCommand : ICommand
    {    
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public Guid CorrelationId { get; set; }        
        public TCommand Command { get; }
        public string ReplyEvent { get; set; }
        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();        

        public CommandContext(TCommand command, string replyEvent = null, Guid? id = null, Guid? requestId = null, Guid? correlationId = null)
        {
            this.Id = id ?? Guid.NewGuid();                       
            this.Command = command;
            if (!string.IsNullOrEmpty(replyEvent)) this.ReplyEvent = replyEvent;
            if (requestId != null) this.RequestId = requestId.Value;
            if (correlationId != null) this.CorrelationId = correlationId.Value;
        }

        public static IMessageContext New(TCommand command)
        {
            return new CommandContext<TCommand>(command);
        }
    }    
}