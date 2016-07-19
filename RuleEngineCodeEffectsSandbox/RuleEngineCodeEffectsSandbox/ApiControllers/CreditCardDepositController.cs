using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CodeEffects.Rule.Core;
using PinnacleSports.RuleRepo.Repository.Interfaces;
using PinnacleSports.RuleService.Models.CreditDeposit;
using RuleEngineCodeEffectsSandbox.Dto;
using RuleEngineCodeEffectsSandbox.Mapping.Interfaces;

namespace RuleEngineCodeEffectsSandbox.ApiControllers
{
    public class CreditCardDepositController : ApiController
    {
        private readonly ICreditCardDepositRepository _creditCardDepositRepository;
        private readonly ICreditCardDepositMapping _creditCardDepositMapping;

        public CreditCardDepositController(ICreditCardDepositRepository creditCardDepositRepository, 
            ICreditCardDepositMapping creditCardDepositMapping)
        {
            _creditCardDepositRepository = creditCardDepositRepository;
            _creditCardDepositMapping = creditCardDepositMapping;
        }
        
        public IHttpActionResult Post([FromBody] CreditCardDepositDto creditCardDepositDto)
        {
            var rulesList = _creditCardDepositRepository.GetRuleFlowSortedList(typeof(CreditCardDepositModel));

            var creditCardModel = _creditCardDepositMapping
                .GetMapper()
                .Map<CreditCardDepositModel>(creditCardDepositDto);

            foreach (var rulesModel in rulesList)
            {
                new Evaluator<CreditCardDepositModel>(rulesModel.Value.XmlRuleFull.ToString())
                    .Evaluate(creditCardModel);

                if (!creditCardModel.IsValid)
                    return Ok(new { creditCardModel.IsValid, Messages = creditCardModel.Notification.Message.ToList() });
            }

            return Ok(new { IsValid = true, Messages = new List<string> { "Transaction is valid." } });
        }
    }
}
