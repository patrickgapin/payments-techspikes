using System.Collections;
using System.Collections.Generic;

namespace RuleEngineCodeEffectsSandbox.Dto
{
    public class CreditCardDepositDto
    {
        public CreditCardDepositDto()
        {
            NotificationMessages = new List<string>();
        }

        public int CustomerId { get; set; }
        public string CcFirstName { get; set; }
        public string CcLastName { get; set; }
        public string CcNumber { get; set; }
        public double Amount { get; set; }

        public IList<string> NotificationMessages { get; set; }
    }
}