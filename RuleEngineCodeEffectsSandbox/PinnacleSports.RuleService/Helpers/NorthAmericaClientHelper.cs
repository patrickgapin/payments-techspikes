using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinnacleSports.RuleService.Helpers
{
    public class NorthAmericaClientHelper: IClientHelper
    {
        public string GetClientRegion()
        {
            return "North America";
        }
    }
}
