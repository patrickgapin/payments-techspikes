using NEventStore.Common.Events;
using NEventStore.Common.Events.Interfaces;
using NEventStore.Common.EventServices;
using NEventStore.Common.EventServices.Interfaces;
using NEventStore.Common.EventStore;
using NEventStore.Common.Ioc.Interfaces;
using NEventStore.Common.Models;
using NEventStore.Common.Models.Interfaces;
using SimpleInjector;

namespace NEventStore.Common.Ioc
{
    public static class SimpleInjectorInitializerCommon
    {
        public enum ImplementationType
        {
            AccountCreated,
            FundsDespoited,
            FundsWithdrawed
        }

        public static void InitializeContainer(Container container)
        {
            container.Register<IBankAccount, BankAccount>();

            var requestHandlerFactory = new RequestHandlerFactory(container);
            requestHandlerFactory.Register<IApplyService<BankAccount>, AccountCreatedService>(ImplementationType.AccountCreated);
            requestHandlerFactory.Register<IApplyService<BankAccount>, FundsDepositedService>(ImplementationType.FundsDespoited);
            requestHandlerFactory.Register<IApplyService<BankAccount>, FundsWithdrawedService>(ImplementationType.FundsWithdrawed);
            container.RegisterSingleton<IRequestHandlerFactory>(requestHandlerFactory);
        }
    }
}