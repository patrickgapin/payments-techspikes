using System;
using NEventStore.Common.EventStore;
using NEventStore.Common.Ioc;
using NEventStore.Read.Services;
using NEventStore.Read.Services.Interfaces;
using SimpleInjector;

namespace NEventStore.Read
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            SimpleInjectorInitializerCommon.InitializeContainer(container);
            container.Register(Connection.CreateSqlConnection, Lifestyle.Singleton);
            container.Register<IEventStoreReadService, EventStoreReadService>();
            container.Verify();

            var resourceId = new Guid("240007c2-c30a-43e6-b939-37567803f7af");

            var eventStore = container.GetInstance<IEventStoreReadService>();
            Console.WriteLine(eventStore.GetBankState(resourceId).GetCurrentBalance());

            Console.Read();
        }
    }
}
