//Write a program that retrieves from the Northwind database all product categories and the
//names of the products in each category. Can you do this with a single SQL query (with table join)?

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.ProductsByCategories
{
    class ProductsByCategories
    {
        static void Main(string[] args)
        {
            SqlConnection dbcon = new SqlConnection("Server=KOLYOBIQCHA; " +
           "Database=NORTHWND; Integrated Security=true");

            dbcon.Open();

            using (dbcon)
            {
                SqlCommand cmdSql = new SqlCommand("Select c.CategoryName as cn, p.ProductName as pn " +
                                                    "FROM Categories c " +
                                                    "Join Products p " +
                                                    "ON c.CategoryID = p.CategoryID " +
                                                    "GROUP BY c.CategoryName, p.ProductName ", dbcon);

                SqlDataReader reader = cmdSql.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine((string)reader["cn"] + " - " +
                                            (string)reader["pn"]);
                    }
                }
            }
        }
    }
}
