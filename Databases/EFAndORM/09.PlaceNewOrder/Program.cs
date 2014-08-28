using NorthwindHW.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Transactions;

namespace _09.PlaceNewOrder
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new NORTHWNDEntities();
            using (db)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var order1 = new Order
                    {
                        CustomerID = "ALFKI",
                        EmployeeID = 1,
                    };
                    var order2 = new Order
                    {
                        CustomerID = "ALFKI",
                        EmployeeID = 2
                    };
                    var order3 = new Order
                    {
                        CustomerID = "ALFKI",
                        EmployeeID = 3
                    };

                    db.Orders.Add(order1);
                    db.Orders.Add(order2);
                    db.Orders.Add(order3);

                    db.SaveChanges();
                    scope.Complete();
                }
            }
        }
    }
}
