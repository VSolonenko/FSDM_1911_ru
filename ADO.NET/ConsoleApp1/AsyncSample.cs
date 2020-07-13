using DataProviderFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class AsyncSample
    {
        private void Fill(int iterations)
        {
            var factory = new DbProviderFactoriesSample();
            var insertRaw = "INSERT INTO Names(Name) VALUES('Vlad') ";
            var expression = "";

            for (int i = 0; i < iterations; i++)
            {
                expression += insertRaw;
            }

            using (var connection = factory.GetConnection())
            {
                connection.Open();
                var fillCommand = connection.CreateCommand();
                fillCommand.CommandText = expression;
                fillCommand.ExecuteNonQuery();
            }

        }

        private Task FillAsync(int iterations)
        {
            var factory = new DbProviderFactoriesSample();
            var expression = "";

            var task = Task.Run(() =>
             {
                 var insertRaw = "INSERT INTO Names(Name) VALUES('Vlad') ";
                 expression = "";

                 for (int i = 0; i < iterations; i++)
                 {
                     expression += insertRaw;
                 }
             }).ContinueWith(t =>
             {
                 
                 var connection = factory.GetConnection();
                 connection.OpenAsync().ContinueWith(_ =>
                 {
                     var fillCommand = connection.CreateCommand();
                     fillCommand.CommandText = expression;
                     fillCommand.ExecuteNonQuery();
                 });
             });

            return task;
        }

        private void Clear()
        {
            var factory = new DbProviderFactoriesSample();
            var expression = "DELETE FROM Names";

            using (var connection = factory.GetConnection())
            {
                connection.Open();
                var clearCommand = connection.CreateCommand();
                clearCommand.CommandText = expression;
                clearCommand.ExecuteNonQuery();
            }
        }

        private Task ClearAsync()
        {
            var factory = new DbProviderFactoriesSample();
            var expression = "DELETE FROM Names";
            var connection = factory.GetConnection();

            //var task = connection.OpenAsync();

            //using (var connection2 = factory.GetConnection())
            //{
            //    connection2.OpenAsync().ContinueWith(_ =>
            //     {
            //         var clearCommand = connection2.CreateCommand();
            //         clearCommand.CommandText = expression;
            //         clearCommand.ExecuteNonQuery();
            //     });
            //}

            return connection.OpenAsync().ContinueWith(_ =>
        {
            var clearCommand = connection.CreateCommand();
            clearCommand.CommandText = expression;
            clearCommand.ExecuteNonQuery();
            connection.Close();
        });
        }


        private void Read()
        {
            var factory = new DbProviderFactoriesSample();
            var expression = "SELECT * FROM Names";

            using (var connection = factory.GetConnection())
            {
                connection.Open();
                var readCommand = connection.CreateCommand();
                readCommand.CommandText = expression;
                var reader = readCommand.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader.GetInt32(0)}\tName: {reader.GetString(1)}");
                }
            }

        }

        private Task ReadAsync()
        {
            var factory = new DbProviderFactoriesSample();
            var expression = "SELECT * FROM Names";
            var connection = factory.GetConnection();

            return connection.OpenAsync().ContinueWith(_ =>
            {
                completed = true;
                var readCommand = connection.CreateCommand();
                readCommand.CommandText = expression;
                readCommand.ExecuteReaderAsync().ContinueWith(t =>
                {
                    while (t.Result.Read())
                    {
                        Console.WriteLine($"ID: {t.Result.GetInt32(0)}\tName: {t.Result.GetString(1)}");
                    }
                    connection.Close();
                }
                );
            });
        }

        public void Start()
        {
            //Console.WriteLine("Clear start");
            //Clear();
            //Console.WriteLine("Clear end");
            //Console.WriteLine("Fill start");
            //Fill(1000);
            //Console.WriteLine("Fill end");
            //Read();

            ClearAsync().ContinueWith(_ => FillAsync(1000).ContinueWith(t => ReadAsync()));
            SomeWork();
        }

        private bool completed = false;

        private void SomeWork()
        {
            while (!completed)
            {
                Console.WriteLine("Some work...");
            }
        }
    }
}
