using DataProviderFactory;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var dbProviderSample = new DbProviderFactoriesSample();
            //dbProviderSample.StartSample();

            var asyncSample = new AsyncSample();
            asyncSample.Start();

            Console.ReadKey();
        }
    }
}
