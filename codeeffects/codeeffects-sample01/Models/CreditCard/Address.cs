
using CodeEffects.Rule.Attributes;

namespace codeeffects_sample01.Models.CreditCard
{
    public class Address
    {
        [Field(DisplayName = "Address Street Number", Description = "ie: 250 Yonge Street")]
        public string StreetNumber { get; set; }

        [Field(DisplayName = "Address City")]
        public string City { get; set; }

        [Field(DisplayName = "Address State")]
        public string State { get; set; }

        [Field(DisplayName = "Address Postal Code")]
        public string PostalCode { get; set; }
    }
}