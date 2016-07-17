using System.Collections;
using System.Collections.Generic;
using PinnacleSports.RuleService.RuleServices.Interfaces;

namespace PinnacleSports.RuleService.RuleServices
{
    public class CreditCardService : ICreditCardService
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