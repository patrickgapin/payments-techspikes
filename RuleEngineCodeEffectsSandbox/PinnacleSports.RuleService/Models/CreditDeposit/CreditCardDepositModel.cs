using System.Collections.Generic;
using CodeEffects.Rule.Attributes;
using PinnacleSports.RuleService.Models.Notification;
using PinnacleSports.RuleService.RuleServices.Interfaces;

namespace PinnacleSports.RuleService.Models.CreditDeposit
{
    public class CreditCardDepositModel
    {
        private readonly ICustomerRuleService _customerRuleService;
        public CreditCardDepositModel(ICustomerRuleService customerRuleService)
        {
            _customerRuleService = customerRuleService;
        }

        public CustomerModel Customer { get; set; }
        public CreditCardModel CreditCard { get; set; }
        public DepositTransactionModel DepositTransaction { get; set; }
        public List<string> BlockedCreditCards { get; set; }

        [ExcludeFromEvaluation]
        public NotificationModel Notification { get; set; }
        
        public bool IsValid { get; set; }

        [Method("Is Passed Monthly Limit", "Call External API to Check if Customer is Passed Monthly Limit.")]
        public bool IsPassedMonthlyLimit(double monthlyLimit)
        {
            return _customerRuleService.IsPassedMonthlyLimit(Customer.CustomerId, DepositTransaction.Amount, monthlyLimit);
        }
        
        [Action("Set is Invalid", "Result that will be returned as Invalid.")]
        public void IsInvalid(string returnMessage)
        {
            IsValid = false;
            Notification.Message.Add(returnMessage);
        }
    }
}