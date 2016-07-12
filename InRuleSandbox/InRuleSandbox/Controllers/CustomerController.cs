using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InRule.Repository.Configuration;
using InRuleSandbox.Models;
using InRuleSandbox.Services;
using InRuleSandbox.Services.Interfaces;

namespace InRuleSandbox.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IInRuleService<Customer> _inRuleService;

        public CustomerController(IInRuleService<Customer> inRuleService)
        {
            _inRuleService = inRuleService;
        }

        public ActionResult Index()
        {
            return View(new RuleExecutionModel<Customer>());
        }

        [HttpPost]
        public ActionResult Index(RuleExecutionModel<Customer> model)
        {
            ModelState.Clear();

            model.RuleApp = "TestRuleApplication";
            model.Entity = "Customer";
            model.ReturnEntity = "Customer";

            _inRuleService.ExecuteRuleRequest(model);
            
            return View(model);
        }
    }
}