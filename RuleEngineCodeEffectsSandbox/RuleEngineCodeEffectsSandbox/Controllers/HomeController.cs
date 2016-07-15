using System;
using System.Web.Mvc;
using AutoMapper;
using CodeEffects.Rule.Core;
using CodeEffects.Rule.Models;
using PinnacleSports.RuleService.Models.CreditDeposit;
using PinnacleSports.RuleService.Models.CreditDeposit.Interfaces;
using PinnacleSports.RuleService.RuleServices;
using RuleEngineCodeEffectsSandbox.Dto;
using RuleEngineCodeEffectsSandbox.Mapping.Interfaces;
using RuleEngineCodeEffectsSandbox.Services.Interfaces;

namespace RuleEngineCodeEffectsSandbox.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRuleService _ruleService;
        private readonly ICreditCardDepositMapping _creditCardDepositMapping;

        public HomeController(IRuleService ruleService,
            ICreditCardDepositMapping creditCardDepositMapping)
        {
            _ruleService = ruleService;
            _creditCardDepositMapping = creditCardDepositMapping;
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

            var rule = ruleEditor.GetRuleXml();
            var evaluator = new Evaluator<CreditCardDepositModel>(rule, _ruleService.LoadRuleXml);

            var creditCardModel = _creditCardDepositMapping
                .GetMapper()
                .Map<CreditCardDepositModel>(creditCardDepositDto);

            evaluator.Evaluate(creditCardModel);

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
            // Load rule from the storage
            var ruleXml = _ruleService.LoadRuleXml(id);

            // Create a new model and store it in the bag
            ViewBag.Rule = RuleModel.Create(ruleXml, typeof(CreditCardDepositModel));

            ViewBag.Message = "The rule is loaded";
            return View("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            // Create a new model and store it in the bag
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