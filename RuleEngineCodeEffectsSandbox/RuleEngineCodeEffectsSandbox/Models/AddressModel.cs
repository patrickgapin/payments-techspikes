using System;
using CodeEffects.Rule.Attributes;

namespace RuleEngineCodeEffectsSandbox.Models
{
    public class AddressModel
    {
        [Field(Max = 50,
            StringComparison = StringComparison.InvariantCultureIgnoreCase)]
        public string Street { get; set; }

        [Field(Max = 30, 
            StringComparison = StringComparison.InvariantCultureIgnoreCase)]
        public string City { get; set; }


        [Field(Max = 6)]
        public string PostalCode { get; set; }

    }
}