using System;
using NEventStore.Common.Events.Interfaces;
using NEventStore.Common.EventStore;
using NEventStore.Common.Ioc;
using NEventStore.Common.Ioc.Interfaces;
using NEventStore.Common.Models;
using SimpleInjector;

namespace NEventStore.Read
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            SimpleInjectorInitializer.InitializeContainer(container);
            container.Verify();
            
            var bankState = new BankAccount(container.GetInstance<IRequestHandlerFactory>());
            var resourceId = new Guid("240007c2-c30a-43e6-b939-37567803f7af");

            using (var store = Connection.CreateSqlConnection())
            using (var stream = store.OpenStream(resourceId, 0))
            {
                foreach (var events in stream.CommittedEvents)
                {
                    bankState.Apply(events.Body as IEventBase);
                }
            }

            Console.WriteLine(bankState.CurrentBalance);
            Console.Read();
        }
    }
}
