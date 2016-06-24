using System;
using NEventStore.Common.Ioc;
using NEventStore.Common.Models;

namespace NEventStore.Common.Events.Interfaces
{
    public interface IEventBase
    {
        Guid ResourceId { get; set; }
        SimpleInjectorInitializer.ImplementationType ServiceImplementationType { get; }
    }
}