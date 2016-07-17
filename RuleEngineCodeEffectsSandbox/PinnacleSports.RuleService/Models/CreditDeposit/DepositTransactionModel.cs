using System;

namespace PinnacleSports.RuleService.Models.CreditDeposit
{
    public class DepositTransactionModel
    {
        public Guid TransactionId { get; set; }
        public double Amount { get; set; }
    }
}