using System;
using System.Collections.Generic;
using System.Linq;
using CodeEffects.Rule.Common;
using PinnacleSports.RuleRepo.Repository.Interfaces;
using RuleEngineCodeEffectsSandbox.Services.Interfaces;

namespace RuleEngineCodeEffectsSandbox.Services
{
    public class RuleService : IRuleService
    {
        private readonly ICreditCardDepositRepository _creditCardDepositRepository;

        public RuleService(ICreditCardDepositRepository creditCardDepositRepository)
        {
            _creditCardDepositRepository = creditCardDepositRepository;
        }

        public List<MenuItem> LoadRulesMenuItems(Type modelType)
        {
            var rules = _creditCardDepositRepository.GetAllRules(modelType);

            return rules.Where(rule => rule.XmlRule != null)
                .Select(rule => new MenuItem(rule.RuleId, rule.Name, rule.Description)).ToList();
        }
        
        public List<MenuItem> GetAllRules(Type modelType)
        {
            var rules = LoadRulesMenuItems(modelType);
            rules.Sort((mi1, mi2) => string.Compare(mi1.DisplayName, mi2.DisplayName, StringComparison.Ordinal));
            return rules;
        }

        public void SaveRule(string ruleId, string ruleXml, bool isEval)
        {
            _creditCardDepositRepository.SaveRule(ruleId, ruleXml, isEval);
        }

        public string LoadRuleXml(string ruleId)
        {
            return _creditCardDepositRepository.LoadRuleXml(ruleId);
        }

        public void DeleteRule(string ruleId, Type modelType)
        {
            _creditCardDepositRepository.DeleteRule(ruleId, modelType);
        }
    }
}