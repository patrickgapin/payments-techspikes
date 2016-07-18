namespace PinnacleSports.RuleService.RuleServices.Interfaces
{
    public interface ICustomerRuleService
    {
        bool IsPassedMonthlyLimit(int customerId, double amount, double monthlyLimit);
    }
}