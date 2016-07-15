namespace PinnacleSports.RuleService.RuleServices.Interfaces
{
    public interface ICreditCardRuleService
    {
        bool IsNameMatchOnCreditCard(int customerId);
        bool IsCreditCardBlocked(string creditCardNumber);
    }
}