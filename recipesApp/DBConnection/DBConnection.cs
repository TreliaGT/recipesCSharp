﻿
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace recipesApp
{
    class DBConnection
    {
        public string getConnectionString()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\V244682\Documents\GitHub\recipesCSharp\recipesApp\recipesDB.mdf;Integrated Security=True";
            return connectionString;
        }

    }
}
