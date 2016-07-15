using PinnacleSports.RuleService.Models.Notification;

namespace PinnacleSports.RuleService.Models.CreditDeposit.Interfaces
{
    public interface ICreditCardDepositModel
    {
        CustomerModel Customer { get; set; }
        CreditCardModel CreditCard { get; set; }
        DepositTransactionModel DepositTransaction { get; set; }
        NotificationModel Notification { get; set; }
        string Output { get; set; }
        bool IsValid { get; set; }
        bool IsPassedMonthlyLimit(double amount);
        bool IsCreditCardBlocked(string creditCardNumber);
        bool IsNameMatchOnCreditCard(int customerId);
        void IsInvalid(string returnMessage);
    }
}