using System.Collections.Generic;

namespace PinnacleSports.RuleRepo.Repository.Interfaces
{
    public interface ICreditCardRepository
    {
        IList<string> GetBlockedCreditCards();
    }
}