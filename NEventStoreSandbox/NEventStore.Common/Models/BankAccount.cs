using System;
using System.Collections.Generic;
using NEventStore.Common.Events.Interfaces;
using NEventStore.Common.EventServices.Interfaces;
using NEventStore.Common.Ioc.Interfaces;
using NEventStore.Common.Models.Interfaces;

namespace NEventStore.Common.Models
{
    public class BankAccount : IBankAccount
    {
        private readonly IRequestHandlerFactory _requestHandlerFactory;
        public BankAccount(IRequestHandlerFactory requestHandlerFactory)
        {
            _requestHandlerFactory = requestHandlerFactory;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentBalance { get; set; }

        public List<Transcation> Transcations = new List<Transcation>();

        public void Apply(IEventBase @event)
        {
            _requestHandlerFactory
                .CreateNew<IApplyService<BankAccount>>(@event.ServiceImplementationType)
                .ApplyValues(this, @event);
        }

        public decimal GetCurrentBalance()
        {
            return CurrentBalance;
        }
    }
}