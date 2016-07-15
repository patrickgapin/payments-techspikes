using System;
using System.Collections.Generic;
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
            var list = new List<MenuItem>();

            var rules = _creditCardDepositRepository.GetAllRules(modelType);

            foreach (var rule in rules)
            {
                if (rule.XmlRule?.Attributes != null)
                {
                    list.Add(new MenuItem(
                        rule.XmlRule.Attributes["id"].Value,
                        rule.XmlRule.SelectSingleNode("x:name", rule.XmlNamespaceManager)?.InnerText,
                        rule.XmlRule.SelectSingleNode("x:description", rule.XmlNamespaceManager) == null ? 
                            null : rule.XmlRule.SelectSingleNode("x:description", rule.XmlNamespaceManager)?.InnerText));
                }
            }
            return list;
        }
        
        public List<MenuItem> GetAllRules(Type modelType)
        {
            // Get both execution and evaluation type rules, merge them, sort them and return the result
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