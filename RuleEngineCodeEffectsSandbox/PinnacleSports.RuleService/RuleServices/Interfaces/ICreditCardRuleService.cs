namespace PinnacleSports.RuleService.RuleServices.Interfaces
{
    public interface ICreditCardRuleService
    {
        bool IsCreditCardBlocked(string creditCardNumber);
    }
}