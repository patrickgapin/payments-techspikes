using CodeEffects.Rule.Attributes;
using PinnacleSports.RuleService.Models.CreditDeposit.Interfaces;
using PinnacleSports.RuleService.Models.Notification;
using PinnacleSports.RuleService.RuleServices.Interfaces;

namespace PinnacleSports.RuleService.Models.CreditDeposit
{
    public class CreditCardDepositModel : ICreditCardDepositModel
    {
        private readonly ICustomerRuleService _customerMonthlyLimit;
        private readonly ICreditCardRuleService _creditCardRuleService;

        public CreditCardDepositModel(ICustomerRuleService customerMonthlyLimit, 
            ICreditCardRuleService creditCardRuleService)
        {
            _customerMonthlyLimit = customerMonthlyLimit;
            _creditCardRuleService = creditCardRuleService;

            Customer = new CustomerModel();
            CreditCard = new CreditCardModel();
            DepositTransaction = new DepositTransactionModel();
            Notification = new NotificationModel();
        }

        public CustomerModel Customer { get; set; }
        public CreditCardModel CreditCard { get; set; }
        public DepositTransactionModel DepositTransaction { get; set; }

        [ExcludeFromEvaluation]
        public NotificationModel Notification { get; set; }
        
        [ExcludeFromEvaluation]
        public string Output { get; set; }

        public bool IsValid { get; set; }

        [Method("Is Passed Monthly Limit", "Call External API to Check if Customer is Passed Monthly Limit.")]
        public bool IsPassedMonthlyLimit(double amount)
        {
            return _customerMonthlyLimit.IsPassedMonthlyLimit(Customer.CustomerId, amount);
        }

        [Method("Is Credit Card Blocked", "Call External API to Check if Credit Card is Blocked.")]
        public bool IsCreditCardBlocked(string creditCardNumber)
        {
            return _creditCardRuleService.IsCreditCardBlocked(creditCardNumber);
        }

        [Method("Is Name Match On Credit Card", "Call External API to Check if Name of Customer Matches CreditCard.")]
        public bool IsNameMatchOnCreditCard(int customerId)
        {
            return _creditCardRuleService.IsNameMatchOnCreditCard(customerId);
        }

        [Action("Set is Invalid", "Result that will be returned as Invalid.")]
        public void IsInvalid(string returnMessage)
        {
            IsValid = false;
            Notification.Message.Add(returnMessage);
        }
    }
}