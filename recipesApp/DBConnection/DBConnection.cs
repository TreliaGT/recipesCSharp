
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace recipesApp
{
    class DBConnection
    {
        public string getConnectionString()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=recipes;Integrated Security=True";
            return connectionString;
        }
    }
}
