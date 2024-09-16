using System;
using Microsoft.Extensions.Configuration; 
namespace stringConnection
{
    class Program
    {
        static void Main(string[] args)
        {

            var connectionString = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();

            // test configurations

            Console.WriteLine(connectionString.GetSection("constr").Value);

        }



    }
}
