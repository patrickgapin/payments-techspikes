using InRule.Runtime;

namespace InRuleSandbox.Factories.Interfaces
{
    public interface IInRuleFactory
    {
        RuleSession RuleSessionCreate(CatalogRuleApplicationReference ruleApplication);

        CatalogRuleApplicationReference CatalogRuleApplicationCreate(string catalogUri, 
            string name,
            string username, 
            string password);
    }
}