using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PinnacleSports.RuleService.Models.CreditDeposit;
using PinnacleSports.RuleService.Repository;
using PinnacleSports.RuleService.RuleServices;
using PinnacleSports.Shared.RuleEngineFactory;
using PinnacleSports.Shared.RuleEngineFactory.Interfaces;

namespace PinnacleSports.RuleServiceTest.RuleService
{
    [TestClass]
    public class CustomerRuleServiceTest
    {
        private Mock<IRuleEngineFactory> _ruleEngineFactorMock;
        private Mock<IDepositTransactionRepository> _depositTransactionRepositoryMock;

        [TestInitialize]
        public void InitializeTest()
        {
            _ruleEngineFactorMock = new Mock<IRuleEngineFactory>();
            _depositTransactionRepositoryMock = new Mock<IDepositTransactionRepository>();
        }

        [TestMethod]
        public void IsPassedMonthlyLimit_ChecksAmountPlusAllDepositsIsGreaterMonthlyLimit_ReturnsTrue()
        {
            //Arrange
            _depositTransactionRepositoryMock.Setup(repository => repository.GetDepositTransactionList(It.IsAny<int>()))
                .Returns(new List<DepositTransactionModel>() {new DepositTransactionModel() {Amount = 100}});

            _ruleEngineFactorMock.Setup(
                factory =>
                    factory.CreateNew<IDepositTransactionRepository>(It.IsAny<RuleEngineTypes.ImplementationType>()))
                .Returns(_depositTransactionRepositoryMock.Object);

            var testModel = new CreditCardDepositModel(_ruleEngineFactorMock.Object)
            {
                DepositTransaction = new DepositTransactionModel() {  Amount = 100},
                Customer = new CustomerModel() 
            };

            var customerRuleService = new CustomerRuleService();

            //Act
            var result = customerRuleService.IsPassedMonthlyLimit(testModel, 199);

            //Assert
            Assert.IsTrue(result);
        }
    }
}