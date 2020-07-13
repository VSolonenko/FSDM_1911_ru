using System;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;

namespace DataProviderFactory
{
    class DbProviderFactoriesSample
    {
        private string provider;
        private string connectionStr;

        public DbProviderFactoriesSample()
        {
            provider = ConfigurationManager.AppSettings["provider"];
            connectionStr = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }

        public void StartSample()
        {
            var factory = DbProviderFactories.GetFactory(provider);
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionStr;
                Console.WriteLine(connection.GetType().FullName);
                connection.Open();
            }
        }

        public DbConnection GetConnection()
        {
            var connection = DbProviderFactories.GetFactory(provider).CreateConnection();
            connection.ConnectionString = connectionStr;
            return connection;
        }
    }
}
