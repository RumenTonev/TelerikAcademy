//Write a method that adds a new product in the products table in the Northwind database.
//Use a parameterized SQL command.

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.AddProducts
{
    class AddProducts
    {
        private SqlConnection dbCon;

        static void Main()
        {
            AddProducts project = new AddProducts();

            try
            {
                project.ConnectToDB();

                project.AddProduct("testProduct",1, 1,"2",20d,2,3,43,true);
            }
            finally
            {
                project.DisconectFromDB();
            }
        }

        private void AddProduct(string ProductName, int? SupplierID, int? CategoryID,
                                string QuantityPerUnit, double? UnitPrice,
                                int? UnitsInStock, int? UnitsOnOrder,
                                int? ReorderLevel, bool Discontinued)
        {

            SqlCommand cmdSql = new SqlCommand("INSERT " +
                                               "INTO Products(ProductName, SupplierID, CategoryID, " + 
                                                            "QuantityPerUnit, UnitPrice, " + 
                                                            "UnitsInStock, UnitsOnOrder, " +
                                                            "ReorderLevel, Discontinued)" +
                                                "VALUES(@prName, @supID, @catID, @qPerUnit, @uPrice, " +
                                                            "@uInSt, @uOnOrd, @reorderL, @disc)", dbCon);

            cmdSql.Parameters.AddWithValue("@prName", ProductName);
            cmdSql.Parameters.AddWithValue("@supID", SupplierID);
            cmdSql.Parameters.AddWithValue("@catID", CategoryID);
            cmdSql.Parameters.AddWithValue("@qPerUnit", QuantityPerUnit);
            cmdSql.Parameters.AddWithValue("@uPrice", UnitPrice);
            cmdSql.Parameters.AddWithValue("@uInSt", UnitsInStock);
            cmdSql.Parameters.AddWithValue("@uOnOrd", UnitsOnOrder);
            cmdSql.Parameters.AddWithValue("@reorderL", ReorderLevel);
            cmdSql.Parameters.AddWithValue("@disc", Discontinued);

            cmdSql.ExecuteNonQuery();
        }

        private void DisconectFromDB()
        {
            if (dbCon != null)
            {
                dbCon.Close();
            }
        }

        private void ConnectToDB()
        {
            dbCon = new SqlConnection(SettingsDBProducts.Default.DBConnectionString);

            dbCon.Open();
        }
    }
}
