using System.Configuration;
using NEventStore.Persistence.Sql;
using NEventStore.Persistence.Sql.SqlDialects;

namespace NEventStore.Common.EventStore
{
    public static class Connection
    {
        public static IStoreEvents CreateSqlConnection()
        {
            var config = new ConfigurationConnectionFactory("NEventStorePoc",
                "system.data.sqlclient",
                ConfigurationManager.AppSettings["EventStoreConn"]);

            return Wireup.Init()
                .UsingSqlPersistence(config)
                .WithDialect(new MsSqlDialect())
                .InitializeStorageEngine()
                .UsingJsonSerialization()
                .Build();
        }
    }
}