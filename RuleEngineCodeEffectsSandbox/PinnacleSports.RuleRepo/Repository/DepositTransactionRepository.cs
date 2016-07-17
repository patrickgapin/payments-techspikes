using System;
using System.Collections;
using System.Collections.Generic;
using PinnacleSports.RuleService.Models.CreditDeposit;
using PinnacleSports.RuleService.Repository;

namespace PinnacleSports.RuleRepo.Repository
{
    public class DepositTransactionRepository : IDepositTransactionRepository
    {
        public IList<DepositTransactionModel> GetDepositTransactionList(int customerId)
        {
            
            return new List<DepositTransactionModel>()
            {
                new DepositTransactionModel()
                {
                    TransactionId = Guid.NewGuid(),
                    Amount = 100
                },
                new DepositTransactionModel()
                {
                    TransactionId = Guid.NewGuid(),
                    Amount = 100
                },
                new DepositTransactionModel()
                {
                    TransactionId = Guid.NewGuid(),
                    Amount = 100
                }
            };
        }
    }
}