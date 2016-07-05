
using System.Collections.Generic;
using codeeffects_sample01.Models;
using codeeffects_sample01.Models.CreditCard;
using codeeffects_sample01.Models.Helpers;
using CodeEffects.Rule.Attributes;

// Dynamic Menu Data Sources
// http://www.codeeffects.com/Doc/Business-Rules-Dynamic-Menu-Data-Sources
// External Method Attribute

// http://codeeffects.com/Doc/Business-Rule-External-Method-Attribute
namespace codeeffects_sample01.Rules.DepositLimits
{
    [Source(DeclaredMembersOnly = true)] // Prevents loading members from another partial class in case there is one which is automatically built. 
                                        // Could apply to a EF-generated class.
    [ExternalAction(typeof(DepositLimitHelper), "Validate")]
    [ExternalMethod(typeof(DepositLimitHelper), "IsCreditCardAmountValid")]
    [ExternalMethod(typeof(Helper), "Count")]
    [Data("roles", typeof(Helper), "GetRoles")]
    [Data("statuses", "getStatuses")]
    [ExternalMethod(typeof(User), "Authorize")]
    public class DepositLimitRule: IDepositimitRule
    {        
        // Using a Reference-type collection will bring the 'Exists' and 'Does not exist' fields in the menu.
        [Field(DisplayName = "Credit Card")]
        public List<CreditCard> Cards { get; set; }

        public CreditCard CreditCard { get; set; }

        public User Client { get; set; }

        public bool CardAmountHigherThan2000() { return CreditCard.Amount > 2000; }

        public Result EvaluationResult { get; set; }

        // This property is used to display outputs of rule actions
        [ExcludeFromEvaluation]
        public string Output { get; set; }

    }

}