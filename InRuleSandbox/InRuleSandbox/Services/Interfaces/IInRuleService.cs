using InRuleSandbox.Models;

namespace InRuleSandbox.Services.Interfaces
{
    public interface IInRuleService<T> 
        where T : new()
    {
        void ExecuteRuleRequest(RuleExecutionModel<T> model);
    }
}