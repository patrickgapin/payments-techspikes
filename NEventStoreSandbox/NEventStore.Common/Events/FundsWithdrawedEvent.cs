using System;
using NEventStore.Common.Events.Interfaces;
using NEventStore.Common.Ioc;
using NEventStore.Common.Models;

namespace NEventStore.Common.Events
{
    public class FundsWithdrawedEvent : IFundsWithdrawedEvent
    {
        public Guid ResourceId { get; set; }
        public decimal Amount { get; set; }
        public SimpleInjectorInitializerCommon.ImplementationType ServiceImplementationType => SimpleInjectorInitializerCommon.ImplementationType.FundsWithdrawed;
    }
}