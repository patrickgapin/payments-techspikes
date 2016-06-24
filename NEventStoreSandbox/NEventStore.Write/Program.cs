using System;
using System.Collections.Generic;
using NEventStore.Common.Events;
using NEventStore.Common.Events.Interfaces;
using NEventStore.Common.EventStore;
using NEventStore.Common.Ioc;
using NEventStore.Write.Services;
using NEventStore.Write.Services.Interfaces;
using SimpleInjector;

namespace NEventStore.Write
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.Register(Connection.CreateSqlConnection, Lifestyle.Singleton);
            container.Register<IEventStoreWriteService, EventStoreWriteService>(Lifestyle.Singleton);
            container.Verify();

            var resourceId = new Guid("240007c2-c30a-43e6-b939-37567803f7af");

            var eventsToRun = new List<IEventBase>
            {
                new AccountCreatedEvent { ResourceId = resourceId, AccountName = "Omid Gerami"},
                new FundsDespoitedEvent { ResourceId = resourceId, Amount = 150 },
                new FundsDespoitedEvent { ResourceId = resourceId, Amount = 100 },
                new FundsWithdrawedEvent { ResourceId = resourceId, Amount = 60 },
                new FundsWithdrawedEvent { ResourceId = resourceId, Amount = 94 },
                new FundsDespoitedEvent { ResourceId = resourceId, Amount = 4 },
            };
            
            using(var eventStoreWriteService = container.GetInstance<IEventStoreWriteService>())
            eventsToRun.ForEach(@event =>
            {
                eventStoreWriteService.WriteEvents(resourceId, @event);
            });

            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
