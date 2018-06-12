using System;
using System.Collections.Generic;

namespace Stone.Sdk.Messaging
{
    public class MessageContext : IMessageContext
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public Guid CorrelationId { get; set; }
        public string ReplyEvent { get; set; }
        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();        
    }
}