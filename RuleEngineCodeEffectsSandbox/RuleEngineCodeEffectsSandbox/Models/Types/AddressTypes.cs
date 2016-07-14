using CodeEffects.Rule.Attributes;

namespace RuleEngineCodeEffectsSandbox.Models.Types
{
    public class AddressTypes
    {
        public enum State
        {
            Arizona,
            California,
            Florida,
            Georgia,
            [EnumItem("North Dakota")]
            NorthDakota,
            [EnumItem("South Carolina")]
            SouthCarolina,
            [ExcludeFromEvaluation]
            Unknown
        }
    }
}