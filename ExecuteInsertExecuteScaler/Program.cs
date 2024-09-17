using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ExecuteInsertExecuteScaler
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
            




            var insertStatment = "INSERT INTO WALLETS (Holder, Balance) VALUES " +
                $"(@Holder, @Balance);"+ 
                $"SELECT CAST(scope_identity() AS int)";

            /*"INSERT INTO WALLETS (Holder, Balance) VALUES " +
                            $"(@Holder, @Balance);" +
                            $"SELECT CAST(scope_identity() AS int)";*/



            // first Parameter

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

            // this line tell complier that command not storded procedure
            command.CommandType = CommandType.Text;

            // open connection to DB 
            connectionToDB.Open();


            NewWallet.Id = (int)command.ExecuteScalar();

            Console.WriteLine($"wallet {NewWallet} added successully");





            connectionToDB.Close();

        }
    }
}
