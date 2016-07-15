using PinnacleSports.RuleService.RuleServices.Interfaces;

namespace PinnacleSports.RuleService.RuleServices
{
    public class CustomerRuleService : ICustomerRuleService
    {
        public bool IsPassedMonthlyLimit(int customerId, double amount)
        {
            return customerId == 10 & amount > 1000;
        }
    }
}