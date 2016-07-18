using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using codeeffects_sample01.Models;
using codeeffects_sample01.Models.CreditCard;
using CodeEffects.Rule.Attributes;
using CodeEffects.Rule.Common;

namespace codeeffects_sample01.Rules.DepositLimits
{
    public class DepositLimitHelper
    {
        public int Test { get; set; }

        public DepositLimitHelper(int test)
        {
            Test = test;
        }

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

        // This public parameterless function will be used as a dynamic source for the property TestNumber declared in our DepositLimitRule Sourceobject.
        public List<DataSourceItem> GetTestNumbers()
        {
            var list = new List<DataSourceItem>();

            // Customize the return of this method
            // based on the value of "test" field
            if (this.Test > 4)
            {
                list.Add(new DataSourceItem(5, "Five"));
                list.Add(new DataSourceItem(6, "Six"));
            }
            else
            {
                list.Add(new DataSourceItem(1, "One"));
                list.Add(new DataSourceItem(2, "Two"));
            }

            return list;
        }
    }
}