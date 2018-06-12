using System;

namespace Stone.Sdk.Messaging
{
    public class Event : IEvent
    {
        public EventType Type => this.Exception != null ? EventType.Failure : EventType.Success;
        public EventException Exception { get; set; }

        public Event()
        {
            
        }

        public Event(Exception exception)
            : this()
        {
            this.Exception = exception;
        }
    }
}