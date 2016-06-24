using NEventStore.Common.Events;
using NEventStore.Common.Events.Interfaces;
using NEventStore.Common.EventServices;
using NEventStore.Common.EventServices.Interfaces;
using NEventStore.Common.Ioc.Interfaces;
using NEventStore.Common.Models;
using SimpleInjector;

namespace NEventStore.Common.Ioc
{
    public static class SimpleInjectorInitializer
    {
        public enum ImplementationType
        {
            AccountCreated,
            FundsDespoited,
            FundsWithdrawed
        }

        public static void InitializeContainer(Container container)
        {
            var requestHandlerFactory = new RequestHandlerFactory(container);
            requestHandlerFactory.Register<IApplyService<BankAccount>, AccountCreatedService>(ImplementationType.AccountCreated);
            requestHandlerFactory.Register<IApplyService<BankAccount>, FundsDepositedService>(ImplementationType.FundsDespoited);
            requestHandlerFactory.Register<IApplyService<BankAccount>, FundsWithdrawedService>(ImplementationType.FundsWithdrawed);
            container.RegisterSingleton<IRequestHandlerFactory>(requestHandlerFactory);
        }
    }
}