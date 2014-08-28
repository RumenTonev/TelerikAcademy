//Write a program that reads a string from the console and finds all products that contain this 
//string. Ensure you handle correctly characters like ', %, ", \ and _.

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08.FindProducts
{
    class FindProducts
    {
        static void Main()
        {
            SqlConnection dbCon = new SqlConnection("Server=KOLYOBIQCHA; " +
           "Database=NORTHWND; Integrated Security=true");
            
            FindAllThatContains(dbCon, "ch");
        }

        private static void FindAllThatContains(SqlConnection dbCon, string search)
        {
            dbCon.Open();

            using (dbCon)
            {
                SqlCommand cmdSql = new SqlCommand("SELECT ProductName FROM Products", dbCon);
                SqlDataReader reader = cmdSql.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        string name = (string)reader["ProductName"];

                        if (name.Contains(search))
                        {
                            Console.WriteLine(name);
                        }
                    }
                }
            }
        }

    
    }
}
