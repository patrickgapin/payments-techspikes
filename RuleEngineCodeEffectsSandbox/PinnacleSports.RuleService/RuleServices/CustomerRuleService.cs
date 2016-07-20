using System.Linq;
using CodeEffects.Rule.Attributes;
using PinnacleSports.RuleService.Models.CreditDeposit;
using PinnacleSports.RuleService.Repository;
using PinnacleSports.RuleService.RuleServices.Interfaces;
using PinnacleSports.Shared.RuleEngineFactory;

namespace PinnacleSports.RuleService.RuleServices
{
    public class CustomerRuleService : ICustomerRuleService
    {
        [Method("Is Passed Monthly Limit", "Call External API to Check if Customer is Passed Monthly Limit.")]
        public bool IsPassedMonthlyLimit(CreditCardDepositModel creditCardDepositModel, double monthlyLimit)
        {
            return creditCardDepositModel.DepositTransaction.Amount + 
                creditCardDepositModel
                .RuleEngineFactory
                .CreateNew<IDepositTransactionRepository>(RuleEngineTypes.ImplementationType.DepositTransactionRepository)
                .GetDepositTransactionList(creditCardDepositModel.Customer.CustomerId)
                .Sum(model => model.Amount) > monthlyLimit;
        }
    }
}