using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace Champion_League.DBConnector
{
    public class DBconnect
    {
        
        string connectionString = "Data Source=(localdb)\\MSSQLLOCALDB;Initial Catalog=LeagueOfChampions;Integrated Security=True";

            public SqlConnection Connect()
            {
                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();
                    Console.WriteLine("Database connection successful.");
                    return connection;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error connecting to database: " + ex.Message);
                    return null;
                }
            }
        
    }


}