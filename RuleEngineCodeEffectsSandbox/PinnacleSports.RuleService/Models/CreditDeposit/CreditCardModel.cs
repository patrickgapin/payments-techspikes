using CodeEffects.Rule.Attributes;
using PinnacleSports.RuleService.RuleServices.Interfaces;

namespace PinnacleSports.RuleService.Models.CreditDeposit
{
    [Source]
    public class CreditCardModel
    {
        public CreditCardModel(string firstName, 
            string lastName,
            string creditCardNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            CreditCardNumber = creditCardNumber;
        }

        [Field(Max = 30)]
        public string FirstName { get; private set; }
        [Field(Max = 30)]
        public string LastName { get; private set; }
        public string CreditCardNumber { get; private set; }
    }
}