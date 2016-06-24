using NEventStore.Common.Events.Interfaces;
using NEventStore.Common.EventServices.Interfaces;
using NEventStore.Common.Models;

namespace NEventStore.Common.EventServices
{
    public class FundsDepositedService : IApplyService<BankAccount>
    {
        public void ApplyValues(BankAccount bank, IEventBase @event)
        {
            var newTranscation = new Transcation { Id = @event.ResourceId, Amount = ((IFundsDespoitedEvent)@event).Amount };
            bank.Transcations.Add(newTranscation);
            bank.CurrentBalance = bank.CurrentBalance + ((IFundsDespoitedEvent)@event).Amount;
        }
    }
}