using System;
using System.Collections.Generic;

namespace Stone.Sdk.Messaging
{
    public interface IMessageContext
    {
        Guid Id { get; set; }
        Guid RequestId { get; set; }
        Guid CorrelationId { get; set; }
        string ReplyEvent { get; set; }
        IDictionary<string, object> Metadata { get; set; }        
    }
}