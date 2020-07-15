using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisconnectedMode
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=localhost;Initial Catalog=ClientsDB;Integrated Security=True";
            var query = "SELECT * FROM Clients";

            var connection = new SqlConnection(connectionString);
           
            var adapter = new SqlDataAdapter(query, connection);
            
            var dataSet = new DataSet();

            //adapter.FillSchema(dataSet, SchemaType.Source, "Clients");
            adapter.Fill(dataSet, "Clients");

            foreach (var row in dataSet.Tables["CLients"].Rows)
            {
                if (row is DataRow dataRow)
                {
                    dataRow.Delete();
                }
            }

            var commandBuilder = new SqlCommandBuilder(adapter);

            adapter.Update(dataSet, "Clients");

            Console.ReadKey();
        }
    }
}
