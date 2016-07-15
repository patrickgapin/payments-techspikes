using CodeEffects.Rule.Attributes;

namespace PinnacleSports.RuleService.Models.CreditDeposit
{
    [Source]
    public class CreditCardModel
    {
        public string CreditCardNumber { get; set; }

        [Field(Max = 30)]
        public string FirstName { get; set; }
        [Field(Max = 30)]
        public string LastName { get; set; }
    }
}