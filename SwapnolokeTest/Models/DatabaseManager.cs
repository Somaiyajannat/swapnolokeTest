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
        private static DatabaseManager instance;

        private String connectionString = ConfigurationManager.ConnectionStrings["swapnolokeDB"].ConnectionString;
        public SqlConnection connection = new SqlConnection();
        public SqlCommand command = new SqlCommand();

        private DatabaseManager()
        {
            connection.ConnectionString = connectionString;
            command.Connection = connection;
        }

        public static DatabaseManager getInstance()
        {
            if (instance == null) instance = new DatabaseManager();
            return instance;
        }
    }
}
