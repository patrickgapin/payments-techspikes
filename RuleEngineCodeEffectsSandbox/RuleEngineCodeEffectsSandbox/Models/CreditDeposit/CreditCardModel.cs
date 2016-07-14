using CodeEffects.Rule.Attributes;

namespace RuleEngineCodeEffectsSandbox.Models.CreditDeposit
{
    [Source]
    public class CreditCardModel
    {
        public string CreditCardNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}