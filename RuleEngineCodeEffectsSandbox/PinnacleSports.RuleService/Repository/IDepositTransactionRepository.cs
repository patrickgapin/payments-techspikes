using System.Collections.Generic;
using PinnacleSports.RuleService.Models.CreditDeposit;

namespace PinnacleSports.RuleService.Repository
{
    public interface IDepositTransactionRepository
    {
        IList<DepositTransactionModel> GetDepositTransactionList(int customerId);
    }
}