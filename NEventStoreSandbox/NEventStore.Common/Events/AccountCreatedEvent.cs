using System;
using NEventStore.Common.Events.Interfaces;
using NEventStore.Common.Ioc;
using NEventStore.Common.Models;

namespace NEventStore.Common.Events
{
    public class AccountCreatedEvent : IAccountCreatedEvent
    {
        public Guid ResourceId { get; set; }
        public string AccountName { get; set; }

        public SimpleInjectorInitializer.ImplementationType ServiceImplementationType => SimpleInjectorInitializer.ImplementationType.AccountCreated;
    }
}