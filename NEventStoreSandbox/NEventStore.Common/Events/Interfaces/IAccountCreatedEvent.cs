namespace NEventStore.Common.Events.Interfaces
{
    public interface IAccountCreatedEvent : IEventBase
    {
        string AccountName { get; set; }
    }
}