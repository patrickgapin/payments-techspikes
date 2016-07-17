using System.Collections.Generic;

namespace PinnacleSports.RuleService.RuleServices.Interfaces
{
    public interface ICreditCardService
    {
        IList<string> GetBlockedCreditCards();
    }
}