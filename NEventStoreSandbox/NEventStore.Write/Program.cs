using System;
using System.Collections.Generic;
using NEventStore.Common.Events;
using NEventStore.Common.Events.Interfaces;
using NEventStore.Common.EventStore;

namespace NEventStore.Write
{
    class Program
    {
        static void Main(string[] args)
        {

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


            using (var store = Connection.CreateSqlConnection())
            using (var stream = store.OpenStream(resourceId, 0))
            {
                foreach (var item in eventsToRun)
                {
                    stream.Add(new EventMessage
                    {
                        Body = item
                    });
                }

                stream.CommitChanges(Guid.NewGuid());
            }
            
            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
