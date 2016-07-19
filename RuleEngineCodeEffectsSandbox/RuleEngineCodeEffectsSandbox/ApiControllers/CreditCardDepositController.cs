using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PinnacleSports.RuleRepo.Repository.Interfaces;
using PinnacleSports.RuleService.Models.CreditDeposit;
using RuleEngineCodeEffectsSandbox.Dto;
using RuleEngineCodeEffectsSandbox.Mapping.Interfaces;
using RuleEngineCodeEffectsSandbox.RuleEngine.Interfaces;

namespace RuleEngineCodeEffectsSandbox.ApiControllers
{
    public class CreditCardDepositController : ApiController
    {
        private readonly ICreditCardDepositRepository _creditCardDepositRepository;
        private readonly ICreditCardDepositMapping _creditCardDepositMapping;
        private readonly IRuleEngineEvaluator _ruleEngineEvaluator;

        public CreditCardDepositController(ICreditCardDepositRepository creditCardDepositRepository, 
            ICreditCardDepositMapping creditCardDepositMapping, 
            IRuleEngineEvaluator ruleEngineEvaluator)
        {
            _creditCardDepositRepository = creditCardDepositRepository;
            _creditCardDepositMapping = creditCardDepositMapping;
            _ruleEngineEvaluator = ruleEngineEvaluator;
        }
        
        public IHttpActionResult Post([FromBody] CreditCardDepositDto creditCardDepositDto)
        {
            var rulesList = _creditCardDepositRepository.GetRuleFlowSortedList(typeof(CreditCardDepositModel));

            var creditCardModel = _creditCardDepositMapping
                .GetMapper()
                .Map<CreditCardDepositModel>(creditCardDepositDto);

            foreach (var rulesModel in rulesList)
            {
                _ruleEngineEvaluator.Evaluate(rulesModel.Value.XmlRuleFull.ToString(), creditCardModel);
                
                if (!creditCardModel.IsValid)
                    return Ok(new { creditCardModel.IsValid, Messages = creditCardModel.Notification.Message.ToList() });
            }

            return Ok(new { IsValid = true, Messages = new List<string> { "Transaction is valid." } });
        }
    }
}
