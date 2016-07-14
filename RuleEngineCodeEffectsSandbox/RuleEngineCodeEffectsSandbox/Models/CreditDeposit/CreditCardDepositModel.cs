using CodeEffects.Rule.Attributes;
using RuleEngineCodeEffectsSandbox.Models.Notification;

namespace RuleEngineCodeEffectsSandbox.Models.CreditDeposit
{
    public class CreditCardDepositModel
    {
        public CreditCardDepositModel()
        {
            Customer = new CustomerModel();
            CreditCard = new CreditCardModel();
            DepositTransaction = new DepositTransactionModel();
            Notification = new NotificationModel();
        }

        public CustomerModel Customer { get; set; }
        public CreditCardModel CreditCard { get; set; }
        public DepositTransactionModel DepositTransaction { get; set; }
        public Notification.NotificationModel Notification { get; set; }
        
        [ExcludeFromEvaluation]
        public string Output { get; set; }

        public bool IsValid { get; set; }

        [Method("Is Passed Monthly Limit", "Call External API to Check if Customer is Passed Monthly Limit.")]
        public bool IsPassedMonthlyLimit(double amount)
        {
            return Customer.CustomerId == 10 & amount > 1000;
        }

        [Method("Is Credit Card Blocked", "Call External API to Check if Credit Card is Blocked.")]
        public bool IsCreditCardBlocked(string creditCardNumber)
        {
            return creditCardNumber == "123456789";
        }

        [Method("Is Name Match On Credit Card", "Call External API to Check if Name of Customer Matches CreditCard")]
        public bool IsNameMatchOnCreditCard(int customerId)
        {
            return customerId < 100;
        }
    }
}