using AutoMapper;
using PinnacleSports.RuleService.Models.CreditDeposit;
using PinnacleSports.RuleService.Models.CreditDeposit.Interfaces;
using PinnacleSports.RuleService.RuleServices;
using PinnacleSports.RuleService.RuleServices.Interfaces;
using RuleEngineCodeEffectsSandbox.Dto;
using RuleEngineCodeEffectsSandbox.Mapping.Interfaces;

namespace RuleEngineCodeEffectsSandbox.Mapping
{
    public class CreditCardDepositMapping : ICreditCardDepositMapping
    {
        private readonly ICreditCardDepositModel _cardDepositModel;

        public CreditCardDepositMapping(ICreditCardDepositModel cardDepositModel)
        {
            _cardDepositModel = cardDepositModel;
        }

        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreditCardDepositDto, CreditCardDepositModel>()
                    .ConstructUsing(
                        dto =>
                        {
                            _cardDepositModel.Customer.CustomerId = dto.CustomerId;
                            _cardDepositModel.CreditCard.FirstName = dto.CcFirstName;
                            _cardDepositModel.CreditCard.LastName = dto.CcLastName;
                            _cardDepositModel.CreditCard.CreditCardNumber = dto.CcNumber;
                            _cardDepositModel.DepositTransaction.Amount = dto.Amount;

                            return (CreditCardDepositModel)_cardDepositModel;
                        });
            });

            return config.CreateMapper();
        }
    }
}