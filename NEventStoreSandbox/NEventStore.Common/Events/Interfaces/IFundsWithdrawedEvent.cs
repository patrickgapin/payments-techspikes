namespace NEventStore.Common.Events.Interfaces
{
    public interface IFundsWithdrawedEvent : IEventBase
    {
        decimal Amount { get; set; }
    }
}