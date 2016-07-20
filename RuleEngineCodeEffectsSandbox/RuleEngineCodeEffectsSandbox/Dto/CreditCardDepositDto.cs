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

        public int ClientRoleId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }

        public string CcFirstName { get; set; }
        public string CcLastName { get; set; }
        public string CcNumber { get; set; }

        public double DepositAmount { get; set; }

        public IList<string> NotificationMessages { get; private set; }
    }
}