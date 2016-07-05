using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using codeeffects_sample01.Models;
using codeeffects_sample01.Models.CreditCard;
using CodeEffects.Rule;
using CodeEffects.Rule.Attributes;

namespace codeeffects_sample01.Models.Helpers
{
    public static class CreditCardHelper
    {
        [Method(DisplayName = "Has a valid amount")]
        public static bool IsCreditCardAmountValid(this CreditCard.CreditCard card)
        {
            var cardsLimitsHelper = new Limits();

            var isMinValid = card.Amount >= cardsLimitsHelper.CardLimits[card.Type].Item1;
            var isMaxValid = card.Amount <= cardsLimitsHelper.CardLimits[card.Type].Item2;

            return isMinValid && isMaxValid;
        }
    }
}