// 2. Create a DAO class with static methods which provide functionality for
//    inserting, modifying and deleting customers. Write a testing class.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindHW.Data
{
    public static class DAO
    {
        public static NORTHWNDEntities db = new NORTHWNDEntities();

        public static void Insert(string customerID, string companyName, string city)
        {
            using (db)
            {
                var newCustomer = new Customer
                {
                    CustomerID = customerID,
                    CompanyName = companyName,
                    City = city
                };

                db.Customers.Add(newCustomer);
                db.SaveChanges();
            }
        }

        public static void Delete(string customerID)
        {
            using (db)
            {
                var customerToRemove = db.Customers.Find(customerID);

                db.Customers.Remove(customerToRemove);
                db.SaveChanges();
            }
        }

        public static void Modify(string customerID, string companyName, string city)
        {
            using (db)
            {
                var customerToModify = db.Customers.Find(customerID);
                customerToModify.CompanyName = companyName;
                customerToModify.City = city;
                db.SaveChanges();
            }
        }
    }
}
