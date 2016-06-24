using System;
using NEventStore.Common.Events.Interfaces;
using NEventStore.Common.Models.Interfaces;
using NEventStore.Read.Services.Interfaces;

namespace NEventStore.Read.Services
{
    public class EventStoreReadService : IEventStoreReadService
    {
        private readonly IBankAccount _bankAccount;
        private readonly IStoreEvents _storeEvents;

        public EventStoreReadService(IBankAccount bankAccount, 
            IStoreEvents storeEvents)
        {
            _bankAccount = bankAccount;
            _storeEvents = storeEvents;
        }

        public IBankAccount GetBankState(Guid resourceId)
        {
            using(_storeEvents)
            using (var stream = _storeEvents.OpenStream(resourceId, 0))
            {
                foreach (var events in stream.CommittedEvents)
                {
                    _bankAccount.Apply(events.Body as IEventBase);
                }
            }

            return _bankAccount;
        }
    }
}