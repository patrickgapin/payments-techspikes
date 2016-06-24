using NEventStore.Common.Events.Interfaces;
using NEventStore.Common.EventServices.Interfaces;
using NEventStore.Common.Models;

namespace NEventStore.Common.EventServices
{
    public class AccountCreatedService : IApplyService<BankAccount>
    {
        public void ApplyValues(BankAccount bank, IEventBase @event)
        {
            bank.Id = @event.ResourceId;
            bank.Name = ((IAccountCreatedEvent)@event).AccountName;
            bank.CurrentBalance = 0;
        }
    }
}