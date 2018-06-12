namespace Stone.Sdk.Messaging
{
    public interface IEvent 
    {
        EventType Type { get; }
        EventException Exception { get; set; }
    }
    
    public enum EventType 
    {
        Success,
        Failure
    }
}