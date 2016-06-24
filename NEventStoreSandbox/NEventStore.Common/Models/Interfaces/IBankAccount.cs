using NEventStore.Common.Events.Interfaces;

namespace NEventStore.Common.Models.Interfaces
{
    public interface IBankAccount
    {
        void Apply(IEventBase @event);
        decimal GetCurrentBalance();
    }
}