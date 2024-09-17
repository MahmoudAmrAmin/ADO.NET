using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json; 



namespace ExecuteUpdateRawSql
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var connectionString = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();



            SqlConnection? connectionToDB = new SqlConnection(connectionString.GetSection("constr").Value);


            // read data to insert it in DB 

            Wallet NewWallet = new Wallet()
            {
                Holder = "mahmoud amr amin",
                Balance = 150000m
            };

            



            var insertStatment = "UPDATE Wallets SET Holder =@Holder , Balance = @Balance " +
                $"WHERE Id = @Id ";



            // first Parameter
            SqlParameter idParameter = new SqlParameter()
            {
                ParameterName = "id",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = 7
            };  

            SqlParameter holderParameter = new SqlParameter()
            {
                ParameterName = "@Holder",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = NewWallet.Holder 
            };

            SqlParameter balanccParamter = new SqlParameter()
            {
                ParameterName = "@Balance", 
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                Value = NewWallet.Balance

            };


            //It is used to represent a SQL statement or stored procedure that will be executed against a SQL Server database.
            SqlCommand command = new SqlCommand(insertStatment, connectionToDB);

            command.Parameters.Add(holderParameter);
            command.Parameters.Add(balanccParamter);
            command.Parameters.Add(idParameter);


            // this line tell complier that command not storded procedure
            command.CommandType = CommandType.Text;

            // open connection to DB 
            connectionToDB.Open();

            if(command.ExecuteNonQuery() >  0 )
            {
                Console.WriteLine($" new wallet  added successfully");
            }
                


            /*SqlDataReader reader = command.ExecuteReader();

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
            }*/

            connectionToDB.Close();

        }
    }
}
