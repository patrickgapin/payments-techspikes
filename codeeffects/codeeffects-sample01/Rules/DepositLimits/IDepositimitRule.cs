
using codeeffects_sample01.Models;
using codeeffects_sample01.Models.CreditCard;
using CodeEffects.Rule.Attributes;

namespace codeeffects_sample01.Rules.DepositLimits
{
    public interface IDepositimitRule
    {
        [ExcludeFromEvaluation]
        Result EvaluationResult { get; set; }

        // Use of in-rule method.
        [Method("-- Card Amount is More than 2000 --")]
        bool CardAmountHigherThan2000();

        CreditCard CreditCard { get; set; }

        User  Client { get; set; }
    }
}