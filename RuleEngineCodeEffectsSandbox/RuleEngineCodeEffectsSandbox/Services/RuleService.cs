using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using CodeEffects.Rule.Common;
using RuleEngineCodeEffectsSandbox.Models.RuleEngine;
using RuleEngineCodeEffectsSandbox.Services.Interfaces;

namespace RuleEngineCodeEffectsSandbox.Services
{
    public class RuleService : IRuleService
    {
        public string GetFilePath(string ruleId, bool isEval)
        {
            return HttpContext.Current.Server.MapPath(
                $"/Rules/{(isEval ? "Evaluation" : "Execution")}/{ruleId}.config");
        }

        public List<MenuItem> LoadRulesMenuItems(bool evaluationType, Type modelType)
        {
            var list = new List<MenuItem>();

            var rules = GetAllRules(evaluationType, modelType);
            foreach (var rule in rules)
            {
                if (rule.XmlRule.Attributes != null)
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

        public List<MenuItem> GetEvaluationRules(Type modelType)
        {
            return LoadRulesMenuItems(true, modelType);
        }
        
        public List<MenuItem> GetAllRules(Type modelType)
        {
            // Get both execution and evaluation type rules, merge them, sort them and return the result
            var rules = LoadRulesMenuItems(true, modelType);
            rules.AddRange(LoadRulesMenuItems(false, modelType));
            rules.Sort((mi1, mi2) => string.Compare(mi1.DisplayName, mi2.DisplayName, StringComparison.Ordinal));
            return rules;
        }

        
        public IList<RulesModel> GetAllRules(bool evaluationType, Type modelType)
        {
            // Get both execution and evaluation type rules, merge them, sort them and return the result
            var path = HttpContext.Current.Server.MapPath(
                $"/Rules/{(evaluationType ? "Evaluation" : "Execution")}/");

            var list = new List<RulesModel>();

            if (!Directory.Exists(path))
                return list;

            foreach (var file in Directory.GetFiles(path))
            {
                var xml = new XmlDocument();
                xml.Load(file);

                var ruleModel = new RulesModel(xml);
                ruleModel.XmlRule = xml.SelectSingleNode($"/x:codeeffects/x:rule[@type='{modelType.AssemblyQualifiedName}']",
                        ruleModel.XmlNamespaceManager);
                list.Add(ruleModel);
            }

            return list;
        }

        public void SaveRule(string ruleId, string ruleXml, bool isEval)
        {
            // Get the path of the rule XML file
            var file = GetFilePath(ruleId, isEval);

            // If a file with this ruleID already exists, delete it
            if (File.Exists(file)) File.Delete(file);

            // Make sure that all necessary directories exist
            Directory.CreateDirectory(Path.GetDirectoryName(file));

            // Save this rule as XML file
            File.WriteAllText(file, ruleXml);
        }

        public string LoadRuleXml(string ruleId)
        {
            // First, check if a file with this rule ID exists in evaluation directory
            var file = GetFilePath(ruleId, true);
            if (File.Exists(file)) return File.ReadAllText(file);
            else
            {
                // Now check if it exists in the execution directory
                file = GetFilePath(ruleId, false);
                if (File.Exists(file)) return File.ReadAllText(file);
            }

            // No such rule found, return null
            return null;
        }

        public void DeleteRule(string ruleId, Type modelType)
        {
            // This is a quick way to check if this rule is referenced in other rules
            var files = GetAllRules(modelType);
            
            if(files.Where(item => item.ID != null)
                    .Select(item => LoadRuleXml(item.ID))
                    .Any(xml => xml.IndexOf(ruleId, StringComparison.Ordinal) > -1))
            {
                throw new Exception("The rule that you are trying to delete is referenced in other rules.");
            }

            // First, check if a file with this rule ID exists in evaluation directory
            var file = GetFilePath(ruleId, true);
            if (File.Exists(file))
            {
                File.Delete(file);
                return;
            }
            // Now check if it exists in the execution directory
            file = GetFilePath(ruleId, false);
            if (!File.Exists(file)) throw new Exception("Could not find the rule with ID " + ruleId);
            File.Delete(file);
            // No such rule found. This is unexpected; throw an exception
        }
    }
}