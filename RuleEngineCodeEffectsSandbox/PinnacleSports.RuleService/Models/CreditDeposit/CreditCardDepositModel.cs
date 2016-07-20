using System.Collections.Generic;
using CodeEffects.Rule.Attributes;
using PinnacleSports.RuleService.Models.Notification;
using PinnacleSports.RuleService.RuleServices;
using PinnacleSports.Shared.RuleEngineFactory.Interfaces;

namespace PinnacleSports.RuleService.Models.CreditDeposit
{
    [ExternalMethod(typeof(CustomerRuleService), "IsPassedMonthlyLimit")]
    public class CreditCardDepositModel
    {
        internal readonly IRuleEngineFactory RuleEngineFactory;
        public CreditCardDepositModel(IRuleEngineFactory ruleEngineFactory)
        {
            RuleEngineFactory = ruleEngineFactory;
        }

        public CustomerModel Customer { get; set; }
        public CreditCardModel CreditCard { get; set; }
        public DepositTransactionModel DepositTransaction { get; set; }
        public List<string> BlockedCreditCards { get; set; }

        [ExcludeFromEvaluation]
        public NotificationModel Notification { get; set; }
        
        public bool IsValid { get; set; }

        [Action("Set is Invalid", "Result that will be returned as Invalid.")]
        public void IsInvalid(string returnMessage)
        {
            IsValid = false;
            Notification.Message.Add(returnMessage);
        }
    }
}