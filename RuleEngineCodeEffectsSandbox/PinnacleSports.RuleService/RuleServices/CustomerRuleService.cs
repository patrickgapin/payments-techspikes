using System.Linq;
using CodeEffects.Rule.Attributes;
using PinnacleSports.RuleService.Repository;
using PinnacleSports.RuleService.RuleServices.Interfaces;

namespace PinnacleSports.RuleService.RuleServices
{
    public class CustomerRuleService : ICustomerRuleService
    {
        private readonly IDepositTransactionRepository _depositTransactionRepository;

        public CustomerRuleService(IDepositTransactionRepository depositTransactionRepository)
        {
            _depositTransactionRepository = depositTransactionRepository;
        }

        public bool IsPassedMonthlyLimit(int customerId, double amount)
        {
            return amount > _depositTransactionRepository.GetDepositTransactionList(customerId).Sum(model => model.Amount);
        }
    }
}