using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SwapnolokeTest.Models
{
    public class DatabaseManager
    {
        private String connectionString = ConfigurationManager.ConnectionStrings["swapnolokeDB"].ConnectionString;
        public SqlConnection connection = new SqlConnection();
        public SqlCommand command = new SqlCommand();

        DatabaseManager()
        {
            connection.ConnectionString = connectionString;
            command.Connection = connection;
        }
    }
}