using System;
using NEventStore.Common.Events.Interfaces;
using NEventStore.Write.Services.Interfaces;

namespace NEventStore.Write.Services
{
    public class EventStoreWriteService : IEventStoreWriteService
    {
        private readonly IStoreEvents _storeEvents;

        public EventStoreWriteService(IStoreEvents storeEvents)
        {
            _storeEvents = storeEvents;
        }

        public void WriteEvents(Guid resourceId, IEventBase @event)
        {
            using (var stream = _storeEvents.OpenStream(resourceId, 0))
            {
                stream.Add(new EventMessage
                {
                    Body = @event
                });

                stream.CommitChanges(Guid.NewGuid());
            }
        }

        public void Dispose()
        {
            _storeEvents?.Dispose();
        }
    }
}