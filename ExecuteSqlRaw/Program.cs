using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Data;



namespace ExecuteSqlRaw
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionString = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();



            SqlConnection ? connectionToDB = new SqlConnection(connectionString.GetSection("constr").Value);

            var selectStatment = "SELECT * FROM WALLETS";

            SqlCommand command = new SqlCommand(selectStatment, connectionToDB);    

            // this line tell complier that command not storded procedure 
            command.CommandType = CommandType.Text;
            
            // open connection to DB 
            connectionToDB.Open();
            

            SqlDataReader reader = command.ExecuteReader();

            Wallet wallet;

            while (reader.Read())
            {
                wallet = new Wallet
                {
                    Id = reader.GetInt32("Id"),
                    Holder = reader.GetString("Holder"),
                    Balance = reader.GetDecimal("Balance"),
                };

                Console.WriteLine(wallet);
            }

            connectionToDB.Close();

        }
    }
}
