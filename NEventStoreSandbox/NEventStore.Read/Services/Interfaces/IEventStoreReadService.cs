using System;
using NEventStore.Common.Models.Interfaces;

namespace NEventStore.Read.Services.Interfaces
{
    public interface IEventStoreReadService
    {
        IBankAccount GetBankState(Guid resourceId);
    }
}