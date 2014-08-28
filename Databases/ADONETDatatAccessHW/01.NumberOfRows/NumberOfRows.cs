//Write a program that retrieves from the Northwind sample database in MS SQL Server the
//number of  rows in the Categories table.

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.NumberOfRows
{
    class NumberOfRows
    {
        static void Main(string[] args)
        {
            //create connection object
            SqlConnection dbCon = new SqlConnection("Server=KOLYOBIQCHA; " +
            "Database=NORTHWND; Integrated Security=true");

            dbCon.Open();

            using (dbCon)
            {
                SqlCommand cmdSql = new SqlCommand("SELECT COUNT(*) FROM Categories", dbCon);
                int rowsCount = (int)cmdSql.ExecuteScalar();
                Console.WriteLine(rowsCount);
            }
        }
    }
}
