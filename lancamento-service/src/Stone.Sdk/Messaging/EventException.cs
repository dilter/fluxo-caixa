using System;

namespace Stone.Sdk.Messaging
{
    public class EventException
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public EventException()
        {
            
        }
        
        public EventException(string message, string stackTrace)
        {
            this.Message = message;
            this.StackTrace = stackTrace;
        }
        
        public static implicit operator EventException(Exception exception)
        {
            return new EventException(exception.Message, exception.StackTrace);
        }
    }
}