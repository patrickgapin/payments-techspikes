using System;
using System.Web.Mvc;
using CodeEffects.Rule.Models;
using PinnacleSports.RuleService.Models.CreditDeposit;
using RuleEngineCodeEffectsSandbox.Dto;
using RuleEngineCodeEffectsSandbox.Mapping.Interfaces;
using RuleEngineCodeEffectsSandbox.RuleEngine.Interfaces;
using RuleEngineCodeEffectsSandbox.Services.Interfaces;

namespace RuleEngineCodeEffectsSandbox.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRuleService _ruleService;
        private readonly ICreditCardDepositMapping _creditCardDepositMapping;
        private readonly IRuleEngineEvaluator _ruleEngineEvaluator;

        public HomeController(IRuleService ruleService,
            ICreditCardDepositMapping creditCardDepositMapping, 
            IRuleEngineEvaluator ruleEngineEvaluator)
        {
            _ruleService = ruleService;
            _creditCardDepositMapping = creditCardDepositMapping;
            _ruleEngineEvaluator = ruleEngineEvaluator;

            LoadMenuRules();
        }

        public ActionResult Index()
        {
            ViewBag.Rule = RuleModel.Create(typeof(CreditCardDepositModel));
            return View(new CreditCardDepositDto());
        }

        [HttpPost]
        public ActionResult Evaluate(CreditCardDepositDto creditCardDepositDto, RuleModel ruleEditor)
        {
            ruleEditor.BindSource(typeof(CreditCardDepositModel));
            ruleEditor.SkipNameValidation = true;
            ViewBag.Rule = ruleEditor;

            if (ruleEditor.IsEmpty() || !ruleEditor.IsValid(_ruleService.LoadRuleXml))
            {
                ViewBag.Message = "The rule is empty or invalid";
                return View("Index", creditCardDepositDto);
            }

            ModelState.Clear();

            var creditCardModel = _creditCardDepositMapping
                .GetMapper()
                .Map<CreditCardDepositModel>(creditCardDepositDto);
            
            _ruleEngineEvaluator.Evaluate(_ruleService.LoadRuleXml(ruleEditor.Id),
                creditCardModel);

            ViewBag.Message = "The rule passed: " + creditCardModel.IsValid;

            foreach (var message in creditCardModel.Notification.Message)
            {
                creditCardDepositDto.NotificationMessages.Add(message);
            }

            return View("Index", creditCardDepositDto);
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
            var ruleXml = _ruleService.LoadRuleXml(id);

            ViewBag.Rule = RuleModel.Create(ruleXml, typeof(CreditCardDepositModel));

            ViewBag.Message = "The rule is loaded";
            return View("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            ViewBag.Rule = RuleModel.Create(typeof(CreditCardDepositModel));

            try
            {
                _ruleService.DeleteRule(id, typeof(CreditCardDepositModel));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Index");
            }

            LoadMenuRules();

            ViewBag.Message = "The rule was deleted successfully";
            return View("Index");
        }

        private void LoadMenuRules()
        {
            ViewBag.ToolBarRules = _ruleService.GetAllRules(typeof(CreditCardDepositModel));
        }
    }
}