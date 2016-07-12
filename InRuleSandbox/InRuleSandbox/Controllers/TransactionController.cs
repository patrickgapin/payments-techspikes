using System.Web.Mvc;
using InRuleSandbox.Models;
using InRuleSandbox.Services.Interfaces;

namespace InRuleSandbox.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IInRuleService<Transaction> _inRuleService;

        public TransactionController(IInRuleService<Transaction> inRuleService)
        {
            _inRuleService = inRuleService;
        }

        public ActionResult Index()
        {
            return View(new RuleExecutionModel<Transaction>());
        }

        [HttpPost]
        public ActionResult Index(RuleExecutionModel<Transaction> ruleExecutionModel)
        {
            ModelState.Clear();

            ruleExecutionModel.RuleApp = "TestRuleApplication";
            ruleExecutionModel.Entity = "Transaction";
            ruleExecutionModel.ReturnEntity = "Transaction";

            _inRuleService.ExecuteRuleRequest(ruleExecutionModel);

            return View(ruleExecutionModel);
        }

    }
}