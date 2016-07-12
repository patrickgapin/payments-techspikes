using InRule.Runtime;
using InRuleSandbox.Factories.Interfaces;

namespace InRuleSandbox.Factories
{
    public class InRuleFactory : IInRuleFactory
    {
        public RuleSession RuleSessionCreate(CatalogRuleApplicationReference ruleApplication)
        {
            return new RuleSession(ruleApplication);
        }

        public CatalogRuleApplicationReference CatalogRuleApplicationCreate(string catalogUri, 
            string name,
            string username, 
            string password)
        {
            return new CatalogRuleApplicationReference(catalogUri, name, username, password);
        }
    }
}