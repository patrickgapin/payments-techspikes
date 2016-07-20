using CodeEffects.Rule.Attributes;
using PinnacleSports.RuleService.RuleServices.Interfaces;

namespace PinnacleSports.RuleService.Models.CreditDeposit
{
    public class ClientModel
    {
        [Field(DisplayName = "Client Role", DataSourceName = "roles")]
        public int ClientRoleID { get; set; }

        
    }
}