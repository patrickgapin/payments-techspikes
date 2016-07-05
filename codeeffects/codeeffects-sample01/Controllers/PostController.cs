using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using codeeffects_sample01.Rules.DepositLimits;
using codeeffects_sample01.Services;
using CodeEffects.Rule.Core;
using CodeEffects.Rule.Models;

namespace codeeffects_sample01.Controllers
{
    public class PostController : Controller
    {
        public PostController()
        {
            ViewBag.Message = "Demo version. Evaluations are delayed for 1 second.";

            this.LoadMenuRules();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Rule = RuleModel.Create(typeof(DepositLimitRule));
            return View();
        }

        [HttpPost]
        public ActionResult Save(RuleModel ruleEditor)
        {
            ruleEditor.BindSource(typeof(DepositLimitRule));

            ViewBag.Rule = ruleEditor;
            var x = CodeEffects.Rule.Common.Xml.GetEmptyRuleDocument();

            if (ruleEditor.IsEmpty() || !ruleEditor.IsValid(StorageService.LoadRuleXml))
            {
                ViewBag.Message = "The rule is empty or invalid";
                return View("Index");
            }

            try
            {
                // Save the rule
                StorageService.SaveRule(
                    ruleEditor.Id,
                    ruleEditor.GetRuleXml(),
                    ruleEditor.IsLoadedRuleOfEvalType == null ?
                        true : (bool)ruleEditor.IsLoadedRuleOfEvalType);

                // Get all rules for Tool Bar and context menus and save it in the bag
                this.LoadMenuRules();

                ViewBag.Message = "The rule was saved successfully";
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            // Save to DB
            return View("Index");
        }

        [HttpGet]
        public ActionResult Load(string id)
        {
            // Load rule from the storage
            string ruleXml = StorageService.LoadRuleXml(id);

            // Create a new model and store it in the bag
            ViewBag.Rule = RuleModel.Create(ruleXml, typeof(DepositLimitRule));

            ViewBag.Message = "The rule is loaded";
            return View("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            // Create a new model and store it in the bag
            ViewBag.Rule = RuleModel.Create(typeof(DepositLimitRule));

            try
            {
                // Delete the rule by its ID
                StorageService.DeleteRule(id);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Index");
            }

            // Refresh Tool Bar and context menus
            this.LoadMenuRules();

            ViewBag.Message = "The rule was deleted successfully";
            return View("Index");

        }

        [HttpPost]
        public ActionResult Evaluate(DepositLimitRule depositLimitRule, RuleModel ruleEditor)
        {
            // Tell Code Effects which type to use as its source object
            ruleEditor.BindSource(typeof(DepositLimitRule));

            // We are not saving the rule, just evaluating it. Tell the model not to enforce the rule name validation
            ruleEditor.SkipNameValidation = true;
            ViewBag.Rule = ruleEditor;

            if (ruleEditor.IsEmpty() || !ruleEditor.IsValid(StorageService.LoadRuleXml))
            {
                ViewBag.Message = "The rule is empty or invalid";
                return View("Index", depositLimitRule);
            }

            // Clear the state (in case value setters are used in this
            // rule and we want to display the new values in the form)
            this.ClearState(typeof(DepositLimitRule), string.Empty);

            // Everything in MVC is hard coded; we need to clear each
            // reference type of our patient separately
            //this.ClearState(typeof(Address), "Home.");
            //this.ClearState(typeof(Address), "Work.");


            // Get rule XML
            string rule = ruleEditor.GetRuleXml();

            // Create an instance of the Evaluator class. Because our rules might reference other rules of evaluation type
            // we use constructor that takes rule's XML and delegate of the method that can load referenced rules by their IDs.
            var evaluator = new Evaluator<DepositLimitRule>(rule, StorageService.LoadRuleXml);

            // Evaluate the patient against the rule
            bool success = evaluator.Evaluate(depositLimitRule);

            if (!string.IsNullOrWhiteSpace(depositLimitRule.Output))
                ViewBag.Message = depositLimitRule.Output;
            else
                ViewBag.Message = "The current rule evaluated to " + success;

            return View("Index", depositLimitRule);
        }

        private void ClearState(Type type, string prefix)
        {
            foreach (PropertyInfo pi in type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public))
                ModelState.Remove(prefix + pi.Name);
        }

        private void LoadMenuRules()
        {
            ViewBag.ToolBarRules = StorageService.GetAllRules();
            ViewBag.ContextMenuRules = StorageService.GetEvaluationRules();
        }
    }
}