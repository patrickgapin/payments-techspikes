using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using codeeffects_sample01.Models;
using codeeffects_sample01.Models.CreditCard;
using CodeEffects.Rule.Attributes;

namespace codeeffects_sample01.Rules.DepositLimits
{
    public class DepositLimitHelper
    {
        public string Test { get; set; }

        // The Parameter passed to an external Action method can be: Value type, String, Enumerable or Source object type.
        [Action(DisplayName = "-- Accept Card --")]
        public void Validate(DepositLimitRule rule)
        {
            rule.CreditCard.EvaluationResult = rule.CreditCard.IsCreditCardAmountValid() ? Result.Pass : Result.Fail;
        }

        // This will be an External Method referenced from our ObjectSource element.
        [Method(DisplayName = " -- Card Amount Is Valid --")]
        public bool IsCreditCardAmountValid(DepositLimitRule rule)
        {
            var cardsLimitsHelper = new Limits();

            var isMinValid = rule.CreditCard.Amount >= cardsLimitsHelper.CardLimits[rule.CreditCard.Type].Item1;
            var isMaxValid = rule.CreditCard.Amount <= cardsLimitsHelper.CardLimits[rule.CreditCard.Type].Item2;

            return isMinValid && isMaxValid;
        }
    }
}