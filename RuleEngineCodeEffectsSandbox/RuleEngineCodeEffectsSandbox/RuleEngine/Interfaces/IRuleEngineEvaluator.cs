using CodeEffects.Rule.Core;

namespace RuleEngineCodeEffectsSandbox.RuleEngine.Interfaces
{
    public interface IRuleEngineEvaluator
    {
        bool Evaluate<T>(string xml, T model);
    }
}