using System;
using CodeEffects.Rule.Attributes;

namespace RuleEngineCodeEffectsSandbox.Models
{
    public class CustomerModel
    {
        public Guid Id { get; set; }

        [Field(DisplayName = "First Name", Description = "Patient's first name", Max = 30)]
        public string FirstName { get; set; }

        [Field(DisplayName = "Last Name", Max = 30, Description = "Patient's last name")]
        public string LastName { get; set; }

        public AddressModel Address { get; set; }
    }
}