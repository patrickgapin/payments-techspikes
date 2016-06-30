using System;
using CodeEffects.Rule.Attributes;

namespace RuleEngineCodeEffectsSandbox.Models
{
    public class ShippingModel
    {
        public Guid Id { get; set; }
        public CustomerModel Customer { get; set; }

        [Field(DisplayName = "Shipping Value", Description = "Total Shipping")]
        public decimal ShippingValue { get; set; }

        [Field(DisplayName = "Is Shipping Free", Description = "Is Shipping going to be free")]
        public bool IsShippingFree { get; set; }

        [ExcludeFromEvaluation]
        public string Output { get; set; }
    }
}