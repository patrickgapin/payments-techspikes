using System;
using System.Reflection;
using System.Web.Mvc;
using CodeEffects.Rule.Core;
using CodeEffects.Rule.Models;
using RuleEngineCodeEffectsSandbox.Models;
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
            ViewBag.Rule = RuleModel.Create(typeof(ShippingModel));
            return View();
        }

        [HttpPost]
        public ActionResult Evaluate(ShippingModel shippingModel, RuleModel ruleEditor)
        {
            ruleEditor.BindSource(typeof(ShippingModel));
            ruleEditor.SkipNameValidation = true;
            ViewBag.Rule = ruleEditor;

            if (ruleEditor.IsEmpty() || !ruleEditor.IsValid(_ruleService.LoadRuleXml))
            {
                ViewBag.Message = "The rule is empty or invalid";
                return View("Index", shippingModel);
            }

            ModelState.Clear();

            var rule = ruleEditor.GetRuleXml();
            var evaluator = new Evaluator<ShippingModel>(rule, _ruleService.LoadRuleXml);

            var success = evaluator.Evaluate(shippingModel);

            if (!string.IsNullOrWhiteSpace(shippingModel.Output))
                ViewBag.Message = shippingModel.Output;
            else
                ViewBag.Message = "The current rule evaluated to " + success;


            return View("Index", shippingModel);
        }

        [HttpPost]
        public ActionResult Save(RuleModel ruleEditor)
        {
            ruleEditor.BindSource(typeof(ShippingModel));
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
            ViewBag.Rule = RuleModel.Create(ruleXml, typeof(ShippingModel));

            ViewBag.Message = "The rule is loaded";
            return View("Index");
        }

        private void LoadMenuRules()
        {
            ViewBag.ToolBarRules = _ruleService.GetAllRules(typeof(ShippingModel));
            ViewBag.ContextMenuRules = _ruleService.GetEvaluationRules(typeof(ShippingModel));
        }

        private void ClearState(IReflect type, string prefix)
        {
            foreach (var pi in type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public))
                ModelState.Remove(prefix + pi.Name);
        }
    }
}