using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Framing;
using Stone.Sdk.Messaging;

namespace Stone.Sdk.Application
{
    public class CommandBus : Bus, ICommandBus
    {
        public CommandBus(AmqpClient amqpClient) 
            : base(amqpClient)
        {
            
        }
        
        private static CommandContext<TCommand> CreateCommandContext<TCommand>(TCommand command, IMessageContext context = null)
            where TCommand : ICommand
        {
            return new CommandContext<TCommand>(command, id: context?.Id, requestId: context?.RequestId,
                correlationId: context?.CorrelationId) {Metadata = context?.Metadata};
        }

        private static string GetQueueName(ICommand command)
        {
            var queuePrefix = command.GetType().FullName.Split('.').GetValue(1).ToString();
            return $"{queuePrefix}.{command.GetType().Name}";
        }

        public async Task SendAsync<TCommand, TReplyEvent>(TCommand command, IMessageContext context = null)
            where TCommand : ICommand
        {
            await SendAsync(command, context, typeof(TReplyEvent));
        } 
        
        public async Task SendAsync<TCommand>(TCommand command, IMessageContext context = null, Type replyEventType = null) 
            where TCommand : ICommand
        {
            var commandContext = CreateCommandContext(command, context);            
            var queueName = GetQueueName(command);
            await _amqpClient.SendMessageAsync(
                queueName: queueName, 
                message: commandContext.Command, 
                basicProperties: new BasicProperties
                {
                    CorrelationId = commandContext.Id.ToString("D"),
                    ReplyTo = commandContext.RequestId.ToString("D"),
                    Headers = commandContext.Metadata,
                    Type = replyEventType != null ? replyEventType.FullName : string.Empty,
                }
            );                        
        }
    }
}