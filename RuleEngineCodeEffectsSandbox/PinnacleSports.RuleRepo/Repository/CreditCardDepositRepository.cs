using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using PinnacleSports.RuleRepo.Helpers;
using PinnacleSports.RuleRepo.Repository.Interfaces;
using PinnacleSports.RuleService.Models.RuleEngine;

namespace PinnacleSports.RuleRepo.Repository
{
    public class CreditCardDepositRepository : ICreditCardDepositRepository
    {
        private static string GetRootPath()
        {
            var path = "";

            var assemblyFile = Path.GetDirectoryName(Assembly.GetCallingAssembly().CodeBase);
            if (!string.IsNullOrEmpty(assemblyFile))
                path = Directory.GetParent(new Uri(assemblyFile).LocalPath).FullName;
            else
            {
                throw new Exception("There is issue with the root Path.");
            }

            return path;
        }

        private static string GetFilePath(string ruleId, bool isEval)
        {

            return
                $"{GetRootPath()}\\Rules\\{(isEval ? "Evaluation" : "Execution")}\\{ruleId}.config";
        }

        public SortedList<int, RulesModel> GetRuleFlowSortedList(Type modelType)
        {
            var ruleList = GetAllRules(modelType);

            var sortedList = new SortedList<int, RulesModel>();
            foreach (var rulesModel in ruleList)
            {
                int index;
                if(rulesModel.Name.IndexOf(".", StringComparison.Ordinal) >= 0 &&
                        int.TryParse(rulesModel.Name.Substring(0, rulesModel.Name.IndexOf(".", StringComparison.Ordinal)), out index))
                    sortedList.Add(index, rulesModel);
            }
            return sortedList;
        }

        public IList<RulesModel> GetAllRules(Type modelType)
        {
            // Get both execution and evaluation type rules, merge them, sort them and return the result
            var fileList = FileHelper.DirectorySearch($"{GetRootPath()}\\Rules\\");

            var list = new List<RulesModel>();

            if (fileList.Count == 0)
                return list;

            list.AddRange(fileList.Select(XDocument.Load)
                .Select(xml => new RulesModel(xml, modelType)));

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
