using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System;
using static System.Console; 

namespace ExecuteDeleteSqlRaw
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
                Holder = "mansour",
                Balance = 160000m
            };





            var deleteStatment = "DELETE FROM Wallets WHERE Id = @Id";

            /*"INSERT INTO WALLETS (Holder, Balance) VALUES " +
                            $"(@Holder, @Balance);" +
                            $"SELECT CAST(scope_identity() AS int)";*/



            // first Parameter
            #region Parameter Section
            SqlParameter idParamter = new SqlParameter()
            {
                ParameterName = "@Id" , 
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,   
                Value  = 2 
            };  


            /*SqlParameter holderParameter = new SqlParameter()
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

            };*/
            #endregion

            //It is used to represent a SQL statement or stored procedure that will be executed against a SQL Server database.
            SqlCommand command = new SqlCommand(deleteStatment, connectionToDB);

            #region container of Parameter

            command.Parameters.Add(idParamter);
            //command.Parameters.Add(holderParameter);
            //command.Parameters.Add(balanccParamter);
            #endregion 


            // this line tell complier that command not storded procedure
            command.CommandType = CommandType.Text;

            // open connection to DB 
            connectionToDB.Open();


            if (command.ExecuteNonQuery() > 0)
            {
                Console.WriteLine($"wallet Deleted  successully");
            }





            connectionToDB.Close();

        }
    }
}
