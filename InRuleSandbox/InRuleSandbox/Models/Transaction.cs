using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InRuleSandbox.Types;

namespace InRuleSandbox.Models
{
    public class Transaction
    {
        public int CustomerId { get; set; }
        public ModelTypes.CreditCardType CreditCardType { get; set; }
        public ModelTypes.SecurityChannelType SecurityChannelType { get; set; }
        public decimal CreditCardFee { get; set; }
        public bool ShouldProcess { get; set; }


        public IEnumerable<SelectListItem> CreditCardList
        {
            get
            {
                return Enum.GetValues(typeof(ModelTypes.CreditCardType)).Cast<ModelTypes.CreditCardType>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList();
            }
        }

        public IEnumerable<SelectListItem> SecurityChannelList
        {
            get
            {
                return Enum.GetValues(typeof(ModelTypes.SecurityChannelType)).Cast<ModelTypes.SecurityChannelType>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList();
            }
        }
    }
}