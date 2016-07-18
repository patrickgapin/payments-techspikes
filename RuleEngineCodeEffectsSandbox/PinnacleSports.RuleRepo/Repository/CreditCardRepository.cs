using System.Collections;
using System.Collections.Generic;
using PinnacleSports.RuleService.Repository;
using ICreditCardRepository = PinnacleSports.RuleRepo.Repository.Interfaces.ICreditCardRepository;

namespace PinnacleSports.RuleRepo.Repository
{
    public class CreditCardRepository : ICreditCardRepository
    {
        public IList<string> GetBlockedCreditCards()
        {
            return new List<string>
            {
                "123456789",
                "987654321"
            };
        }
    }
}