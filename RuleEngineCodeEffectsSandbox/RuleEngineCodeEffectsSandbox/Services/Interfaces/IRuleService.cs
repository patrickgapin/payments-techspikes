using System;
using System.Collections.Generic;
using CodeEffects.Rule.Common;

namespace RuleEngineCodeEffectsSandbox.Services.Interfaces
{
    public interface IRuleService
    {
        List<MenuItem> GetAllRules(Type modelType);

        void SaveRule(string ruleId, string ruleXml, bool isEval);

        string LoadRuleXml(string ruleId);

        void DeleteRule(string ruleId, Type modelType);
    }
}