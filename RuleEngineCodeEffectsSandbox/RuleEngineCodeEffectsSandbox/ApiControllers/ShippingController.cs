using System.Linq;
using System.Web.Http;
using CodeEffects.Rule.Core;
using RuleEngineCodeEffectsSandbox.Models;
using RuleEngineCodeEffectsSandbox.Models.Api;
using RuleEngineCodeEffectsSandbox.Services.Interfaces;

namespace RuleEngineCodeEffectsSandbox.ApiControllers
{
    public class ShippingController : ApiController
    {
        private readonly IRuleService _ruleService;

        public ShippingController(IRuleService ruleService)
        {
            _ruleService = ruleService;
        }

        public IHttpActionResult Post([FromBody]ShippingModel shippingModel)
        {
            var rules = _ruleService.GetAllRules(false, typeof(ShippingModel));

            var isValid = rules.Where(node => node.XmlRule.ParentNode != null)
                .Select(node => new Evaluator<ShippingModel>(node.XmlRule.ParentNode.OuterXml))
                .Any(evaluator => evaluator.Evaluate(shippingModel));
            
            return Ok(new ShippingResponse<ShippingModel>
            {
                IsValid = isValid,
                ResponseModel = shippingModel
            });
        }
    }
}
