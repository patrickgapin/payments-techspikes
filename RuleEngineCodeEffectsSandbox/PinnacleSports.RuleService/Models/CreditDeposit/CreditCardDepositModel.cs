using System.Collections.Generic;
using CodeEffects.Rule.Attributes;
using PinnacleSports.RuleService.Models.Notification;
using PinnacleSports.RuleService.RuleServices.Interfaces;

namespace PinnacleSports.RuleService.Models.CreditDeposit
{
    public class CreditCardDepositModel
    {
        private readonly ICustomerRuleService _customerRuleService;
        private readonly ICreditCardRuleService _creditCardRuleService;
        public CreditCardDepositModel(ICustomerRuleService customerRuleService, 
            ICreditCardRuleService creditCardRuleService)
        {
            _customerRuleService = customerRuleService;
            _creditCardRuleService = creditCardRuleService;
        }

        public CustomerModel Customer { get; set; }
        public CreditCardModel CreditCard { get; set; }
        public DepositTransactionModel DepositTransaction { get; set; }
        public List<string> BlockedCreditCards { get; set; }

        [ExcludeFromEvaluation]
        public NotificationModel Notification { get; set; }
        
        public bool IsValid { get; set; }

        [Method("Is Passed Monthly Limit", "Call External API to Check if Customer is Passed Monthly Limit.")]
        public bool IsPassedMonthlyLimit()
        {
            return _customerRuleService.IsPassedMonthlyLimit(Customer.CustomerId, DepositTransaction.Amount);
        }

        [Method("Is Credit Card Blocked", "Call External API to Check if Credit Card is Blocked.")]
        public bool IsCreditCardBlocked()
        {
            return _creditCardRuleService.IsCreditCardBlocked(CreditCard.CreditCardNumber);
        }

        [Action("Set is Invalid", "Result that will be returned as Invalid.")]
        public void IsInvalid(string returnMessage)
        {
            IsValid = false;
            Notification.Message.Add(returnMessage);
        }
    }
}