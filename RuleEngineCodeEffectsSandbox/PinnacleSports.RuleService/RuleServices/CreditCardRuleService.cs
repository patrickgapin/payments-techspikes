using PinnacleSports.RuleService.RuleServices.Interfaces;

namespace PinnacleSports.RuleService.RuleServices
{
    public class CreditCardRuleService : ICreditCardRuleService
    {
        public bool IsNameMatchOnCreditCard(int customerId)
        {
            return customerId < 100;
        }

        public bool IsCreditCardBlocked(string creditCardNumber)
        {
            return creditCardNumber == "123456789";
        }
    }
}