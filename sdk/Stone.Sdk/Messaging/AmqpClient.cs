using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing;

namespace Stone.Sdk.Messaging
{
    public class AmqpClient
    {
        private readonly IConnection _connection;

        public AmqpClient(IConnection connection)
        {
            _connection = connection;
        }

        private byte[] PrepareMessage(object message)
        {
            try
            {
                var json = JsonConvert.SerializeObject(message, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Error = (sender, args) => throw args.ErrorContext.Error,                
                });
                return Encoding.UTF8.GetBytes(json);
            }
            catch
            {
                return new byte[1];
            }
        }
        
        protected IModel CreateChannel()
        {
            return _connection.CreateModel();
        }

        public async Task SendMessageAsync(string queueName = "",
            string routingKey = "",
            string exchangeName = "",
            string exchangeType = "",
            object message = null, 
            BasicProperties basicProperties = null,
            bool durable = false,
            bool exclusive = false,
            bool autoDelete = false,
            IDictionary<string, object> arguments = null)
        {

            var body = this.PrepareMessage(message);
            using (var channel = this.CreateChannel())
            {
                if (!string.IsNullOrEmpty(exchangeName))
                {
                    channel.ExchangeDeclare(exchangeName, exchangeType);
                }

                if (!string.IsNullOrEmpty(queueName))
                {
                    channel.QueueDeclare(
                        queue: queueName, 
                        durable: durable, 
                        exclusive: exclusive,
                        autoDelete: autoDelete, 
                        arguments: arguments                        
                    );    
                }
                                
                channel.BasicPublish(
                    exchange: exchangeName, 
                    routingKey: string.IsNullOrEmpty(routingKey) ? queueName : routingKey, 
                    basicProperties: basicProperties,
                    body: body
                );
            }
        }        
    }
}