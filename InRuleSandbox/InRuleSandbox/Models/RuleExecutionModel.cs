using System.Collections.Generic;

namespace InRuleSandbox.Models
{
    public class RuleExecutionModel<T> where T : new()
    {
        public RuleExecutionModel()
        {
            Response = new List<string>();
            EntityObject = new T();
        }

        public string RuleApp { get; set; }

        public string RuleSet { get; set; }

        public string Entity { get; set; }

        public T EntityObject { get; set; }

        public string ReturnEntity { get; set; }

        public List<string> Response { get; set; }

    }
}