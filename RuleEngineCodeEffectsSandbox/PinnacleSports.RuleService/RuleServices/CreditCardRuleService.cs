using CodeEffects.Rule.Attributes;
using PinnacleSports.RuleService.RuleServices.Interfaces;

namespace PinnacleSports.RuleService.RuleServices
{
    public class CreditCardRuleService : ICreditCardRuleService
    {
        public bool IsCreditCardBlocked(string creditCardNumber)
        {
            return creditCardNumber == "123456789";
        }
    }
}