using System;
using CodeEffects.Rule.Attributes;

namespace RuleEngineCodeEffectsSandbox.Models.CreditDeposit
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }

        [Field(DisplayName = "First Name", Max = 30)]
        public string FirstName { get; set; }

        [Field(DisplayName = "Last Name", Max = 30)]
        public string LastName { get; set; }
    }
}