using System.Linq;
using AutoMapper;
using PinnacleSports.RuleRepo.Repository.Interfaces;
using PinnacleSports.RuleService.Models.CreditDeposit;
using PinnacleSports.RuleService.Models.Notification;
using PinnacleSports.Shared.RuleEngineFactory.Interfaces;
using RuleEngineCodeEffectsSandbox.Dto;
using RuleEngineCodeEffectsSandbox.Mapping.Interfaces;

namespace RuleEngineCodeEffectsSandbox.Mapping
{
    public class CreditCardDepositMapping : ICreditCardDepositMapping
    {
        private readonly IRuleEngineFactory _ruleEngineFactory;
        private readonly ICreditCardRepository _creditCardRepository; 
	private readonly ICustomerRuleService _customerRuleService;

        public CreditCardDepositMapping(IRuleEngineFactory ruleEngineFactory, ICustomerRuleService customerRuleService,
            ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
            _ruleEngineFactory = ruleEngineFactory;
        }

        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreditCardDepositDto, CreditCardDepositModel>()
                    .ConstructUsing(
                        dto => new CreditCardDepositModel(_ruleEngineFactory)
                        {
                            Client = new ClientModel { ClientRoleID = dto.ClientRoleId },
                            Customer = new CustomerModel()
                            {
                                CustomerId = dto.CustomerId,
                                FirstName = dto.CustomerFirstName,
                                LastName = dto.CustomerLastName
                            },
                            CreditCard = new CreditCardModel(dto.CcFirstName,
                                dto.CcLastName,
                                dto.CcNumber),
                            DepositTransaction = new DepositTransactionModel()
                            {
                                Amount = dto.DepositAmount
                            },
                            BlockedCreditCards = _creditCardRepository.GetBlockedCreditCards().ToList(),
                            Notification = new NotificationModel()
                        });
            });

            return config.CreateMapper();
        }
    }
}