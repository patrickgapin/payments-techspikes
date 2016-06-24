using NEventStore.Common.Events.Interfaces;
using NEventStore.Common.Models;

namespace NEventStore.Common.EventServices.Interfaces
{
    public interface IApplyService<T>
    {
        void ApplyValues(T bank, IEventBase @event);
    }
}