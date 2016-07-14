using System;
using System.Reflection;
using System.Web.Mvc;
using CodeEffects.Rule.Core;
using CodeEffects.Rule.Models;
using RuleEngineCodeEffectsSandbox.Models;
using RuleEngineCodeEffectsSandbox.Models.CreditDeposit;
using RuleEngineCodeEffectsSandbox.Services.Interfaces;

namespace RuleEngineCodeEffectsSandbox.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRuleService _ruleService;
        public HomeController(IRuleService ruleService)
        {
            _ruleService = ruleService;
            LoadMenuRules();
        }

        public ActionResult Index()
        {
            ViewBag.Rule = RuleModel.Create(typeof(CreditCardDepositModel));
            return View(new CreditCardDepositModel());
        }

        [HttpPost]
        public ActionResult Evaluate(CreditCardDepositModel creditCardDepositModel, RuleModel ruleEditor)
        {
            ruleEditor.BindSource(typeof(CreditCardDepositModel));
            ruleEditor.SkipNameValidation = true;
            ViewBag.Rule = ruleEditor;

            if (ruleEditor.IsEmpty() || !ruleEditor.IsValid(_ruleService.LoadRuleXml))
            {
                ViewBag.Message = "The rule is empty or invalid";
                return View("Index", creditCardDepositModel);
            }

            ModelState.Clear();

            var rule = ruleEditor.GetRuleXml();
            var evaluator = new Evaluator<CreditCardDepositModel>(rule, _ruleService.LoadRuleXml);

            evaluator.Evaluate(creditCardDepositModel);

            ViewBag.Message = "The rule passed: " + creditCardDepositModel.IsValid;
            
            return View("Index", creditCardDepositModel);
        }

        [HttpPost]
        public ActionResult Save(RuleModel ruleEditor)
        {
            ruleEditor.BindSource(typeof(CreditCardDepositModel));
            ViewBag.Rule = ruleEditor;

            if (ruleEditor.IsEmpty() || !ruleEditor.IsValid(_ruleService.LoadRuleXml))
            {
                ViewBag.Message = "The rule is empty or invalid";
                return View("Index");
            }

            try
            {
                _ruleService.SaveRule(
                    ruleEditor.Id,
                    ruleEditor.GetRuleXml(),
                    ruleEditor.IsLoadedRuleOfEvalType ?? true);

                LoadMenuRules();

                ViewBag.Message = "The rule was saved successfully";
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View("Index");
        }

        [HttpGet]
        public ActionResult Load(string id)
        {
            // Load rule from the storage
            var ruleXml = _ruleService.LoadRuleXml(id);

            // Create a new model and store it in the bag
            ViewBag.Rule = RuleModel.Create(ruleXml, typeof(CreditCardDepositModel));

            ViewBag.Message = "The rule is loaded";
            return View("Index");
        }

        private void LoadMenuRules()
        {
            ViewBag.ToolBarRules = _ruleService.GetAllRules(typeof(CreditCardDepositModel));
            ViewBag.ContextMenuRules = _ruleService.GetEvaluationRules(typeof(CreditCardDepositModel));
        }

        private void ClearState(IReflect type, string prefix)
        {
            foreach (var pi in type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public))
                ModelState.Remove(prefix + pi.Name);
        }
    }
}