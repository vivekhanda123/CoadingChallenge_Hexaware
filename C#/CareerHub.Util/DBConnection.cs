using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.Util
{
    public class DBConnection
    {
        public static SqlConnection getDBConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyTrainingConnection"].ConnectionString;

            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                return conn;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw new Exception("An unexpected error occurred while connecting to the database.", ex);
            }
            
        }
    }
}
