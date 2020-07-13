using System.Configuration;
using System.Data.Common;
using System.Diagnostics;

namespace DataProviderFactory
{
    class DbProviderFactoriesSample
    {
        public void Start()
        {
            var provider = ConfigurationManager.AppSettings["provider"];
            var connectionStr = ConfigurationManager.AppSettings["connectionString"];

            var factory = DbProviderFactories.GetFactory(provider);

            using(var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionStr;
                connection.Open();
                Debug.Print(connection.GetType().FullName);
            }

        }
    }
}
