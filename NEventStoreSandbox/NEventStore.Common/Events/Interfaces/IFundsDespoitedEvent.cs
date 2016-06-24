namespace NEventStore.Common.Events.Interfaces
{
    public interface IFundsDespoitedEvent : IEventBase
    {
        decimal Amount { get; set; }
    }
}