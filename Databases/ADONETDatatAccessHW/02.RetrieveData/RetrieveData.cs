//Write a program that retrieves the name and description 
//of all categories in the Northwind DB.

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.RetrieveData
{
    class RetrieveData
    {
        static void Main(string[] args)
        {
            SqlConnection dbCon = new SqlConnection("Server=KOLYOBIQCHA; " +
           "Database=NORTHWND; Integrated Security=true");

            dbCon.Open();

            using (dbCon)
            {
                SqlCommand cmdSql = new SqlCommand("SELECT * FROM Categories", dbCon);
                SqlDataReader reader = cmdSql.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        string name = (string)reader["CategoryName"];
                        string desc = (string)reader["Description"];
                        Console.WriteLine("{0} -> {1}", name,desc);
                    }
                }
            }
        }
    }
}
