using System;
using NEventStore.Common.Events.Interfaces;

namespace NEventStore.Write.Services.Interfaces
{
    public interface IEventStoreWriteService : IDisposable
    {
        void WriteEvents(Guid resourceId, IEventBase @event);
    }
}