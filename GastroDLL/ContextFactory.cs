using System;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.Contracts;
using GastroDLL;

namespace Gastro
{
    public class ContextFactory : IDbContextFactory<Gastro>
    {
        private readonly string _connectionStringName;        
        public ContextFactory(string connectionStringName)
        {
            Contract.Requires<NullReferenceException>(
                !string.IsNullOrEmpty(connectionStringName),
                "connectionStringName");
            _connectionStringName = connectionStringName;
        }

        public Gastro Create()
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[_connectionStringName];

            var connection = DbProviderFactories
                .GetFactory(connectionStringSettings.ProviderName)
                .CreateConnection();

            if (connection == null)
            {
                var message = string.Format(
                    "Provider '{0}' could not be used",
                    connectionStringSettings.ProviderName);
                throw new NullReferenceException(message);
            }

            connection.ConnectionString = connectionStringSettings
                .ConnectionString;
            return new Gastro();
        }

        public bool CheckDatabase(bool initialize, Gastro context)
        {
            if (!context.Database.Exists())
            {
                if (initialize)
                {
                    if (!context.Database.CreateIfNotExists())
                        throw new System.Exception("Database Model creation failed");
                }
                else
                    throw new System.Exception("Database Model not exists");
            }
            if (!context.Database.CompatibleWithModel(false))
            {
                throw new System.Exception("Database Model is incompatible");
            }
            return true;
        }

        public Gastro GetContext(bool detect = false)
        {
            Gastro context = Create();
            context.Configuration.ProxyCreationEnabled = false;
            context.Configuration.AutoDetectChangesEnabled = detect;
            return context;
        }
    }
}
