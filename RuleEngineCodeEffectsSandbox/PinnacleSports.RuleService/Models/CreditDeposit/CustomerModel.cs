using CodeEffects.Rule.Attributes;
using PinnacleSports.RuleService.RuleServices.Interfaces;

namespace PinnacleSports.RuleService.Models.CreditDeposit
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
    }
}