//Create a database called NorthwindTwin with the same structure as Northwind using the features 
//from DbContext. Find for the API for schema generation in MSDN or in Google.

using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindHW.Data;
using System.Globalization;

namespace _06.NorthwindTwin
{
    class Program
    {
        static void Main()
        {
            IObjectContextAdapter context = new NORTHWNDEntities();
            string cloneNorthwind = context.ObjectContext.CreateDatabaseScript();

            string createNorthwindCloneDB = "CREATE DATABASE NorthwindTWIN ON PRIMARY " +
            "(NAME = NorthwindTWIN, " +
            "FILENAME = 'D:\\NorthwindTWIN.mdf', " +
            "SIZE = 5MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
            "LOG ON (NAME = NorthwindTWINLog, " +
            "FILENAME = 'D:\\NorthwindTWIN.ldf', " +
            "SIZE = 1MB, " +
            "MAXSIZE = 5MB, " +
            "FILEGROWTH = 10%)";

            SqlConnection dbConForCreatingDB = new SqlConnection(
                "Server=LOCALHOST; " +
                "Database=master; " +
                "Integrated Security=true");

            dbConForCreatingDB.Open();

            using (dbConForCreatingDB)
            {
                SqlCommand createDB = new SqlCommand(createNorthwindCloneDB, dbConForCreatingDB);
                createDB.ExecuteNonQuery();
            }

            SqlConnection dbConForCloning = new SqlConnection(
                "Server=LOCALHOST; " +
                "Database=NorthwindTWIN; " +
                "Integrated Security=true");

            dbConForCloning.Open();

            using (dbConForCloning)
            {
                SqlCommand cloneDB = new SqlCommand(cloneNorthwind, dbConForCloning);
                cloneDB.ExecuteNonQuery();
            }
        }
    }
}
