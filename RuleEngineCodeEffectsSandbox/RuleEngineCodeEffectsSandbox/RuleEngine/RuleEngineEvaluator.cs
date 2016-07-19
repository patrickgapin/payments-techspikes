using CodeEffects.Rule.Core;
using RuleEngineCodeEffectsSandbox.RuleEngine.Interfaces;

namespace RuleEngineCodeEffectsSandbox.RuleEngine
{
    public class RuleEngineEvaluator : IRuleEngineEvaluator
    {
        public bool Evaluate<T>(string xml, T model)
        {
            return new Evaluator<T>(xml).Evaluate(model);
        }
    }
}