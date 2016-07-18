using System;
using System.Collections.Generic;
using PinnacleSports.RuleService.Models.RuleEngine;

namespace PinnacleSports.RuleRepo.Repository.Interfaces
{
    public interface ICreditCardDepositRepository
    {
        SortedList<int, RulesModel> GetRuleFlowSortedList(Type modelType);
        IList<RulesModel> GetAllRules(Type modelType);
        void SaveRule(string ruleId, string ruleXml, bool isEval);
        string LoadRuleXml(string ruleId);
        void DeleteRule(string ruleId, Type modelType);
    }
}