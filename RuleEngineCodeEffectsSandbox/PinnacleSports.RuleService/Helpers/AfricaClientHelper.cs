using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinnacleSports.RuleService.Helpers
{
    public class AfricaClientHelper : IClientHelper
    {
        public string GetClientRegion()
        {
            return "Africa Region";
        }
    }
}
