using System;
using System.Collections.Generic;

namespace Stone.Sdk.Messaging
{
    public class EventContext<TEvent> : IEventContext
        where TEvent : IEvent
    {    
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public Guid CorrelationId { get; set; }
        public string ReplyEvent { get; set; }
        public TEvent Event { get; }
        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public EventContext(TEvent @event, Guid? id, Guid? requestId, Guid? correlationId)
        {
            this.Id = id ?? Guid.NewGuid();            
            this.Event = @event;
            if (requestId != null) this.RequestId = requestId.Value;
            if (correlationId != null) this.CorrelationId = correlationId.Value;
        }
    }
}