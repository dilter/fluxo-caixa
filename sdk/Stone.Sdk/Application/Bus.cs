using System;
using System.Threading.Tasks;
using RabbitMQ.Client.Framing;
using Stone.Sdk.Messaging;

namespace Stone.Sdk.Application
{
    public class Bus
    {        
        protected readonly AmqpClient _amqpClient;                
        protected Bus(AmqpClient amqpClient)
        {
            _amqpClient = amqpClient;                        
        }

        protected async Task TrackCommandAsync<TCommand>(string queueName, string commandName, CommandContext<TCommand> commandContext) 
            where TCommand : ICommand
        {
            try
            {
                await _amqpClient.SendMessageAsync(queueName: queueName, message: commandContext.Command, 
                    basicProperties: new BasicProperties
                    {                        
                        Type = commandName,
                        CorrelationId = commandContext.Id.ToString("D"),
                        Headers = commandContext.Metadata,
                    }
                );
            }
            catch (Exception e)
            {
                throw e;
            }   
        }
    }
}