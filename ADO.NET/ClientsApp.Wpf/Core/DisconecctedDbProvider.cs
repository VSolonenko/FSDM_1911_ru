using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace ClientsApp.Wpf.Core
{
    class DisconnectedDbProvider
    {
        private const string clientsTable = "Clients";

        private readonly string connectionString;
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataSet dataSet;

        private DataSet DataSet { get => dataSet == null ? InitData() : dataSet ; set => dataSet = value; }

        private DataSet InitData()
        {
            var query = "SELECT * FROM Clients";
            connection = new SqlConnection(connectionString);
            adapter = new SqlDataAdapter(query, connection);
            dataSet = new DataSet();
            var commandBuilder = new SqlCommandBuilder(adapter);

            adapter.Fill(dataSet, clientsTable);

            return dataSet;
        }

        public DisconnectedDbProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataView GetView() => DataSet.Tables[clientsTable].DefaultView;

        public void Apply() => adapter.Update(dataSet, clientsTable);
    }
}
