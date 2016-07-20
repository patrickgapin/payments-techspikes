using PinnacleSports.RuleService.Models.CreditDeposit;

namespace PinnacleSports.RuleService.RuleServices.Interfaces
{
    public interface ICustomerRuleService
    {
        bool IsPassedMonthlyLimit(CreditCardDepositModel creditCardDepositModel, double monthlyLimit);
    }
}