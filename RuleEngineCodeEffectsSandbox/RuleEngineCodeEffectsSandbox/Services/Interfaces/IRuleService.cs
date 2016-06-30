using System;
using System.Collections.Generic;
using System.Xml;
using CodeEffects.Rule.Common;
using RuleEngineCodeEffectsSandbox.Models.RuleEngine;

namespace RuleEngineCodeEffectsSandbox.Services.Interfaces
{
    public interface IRuleService
    {
        List<MenuItem> GetEvaluationRules(Type modelType);

        List<MenuItem> GetAllRules(Type modelType);

        IList<RulesModel> GetAllRules(bool evaluationType, Type modelType);

        void SaveRule(string ruleId, string ruleXml, bool isEval);

        string LoadRuleXml(string ruleId);

        void DeleteRule(string ruleId, Type modelType);
    }
}