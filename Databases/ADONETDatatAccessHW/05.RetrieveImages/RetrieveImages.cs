//Write a program that retrieves the images for all categories in the Northwind database 
//and stores them as JPG files in the file system.


using System;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;

namespace _05.RetrieveImages
{
    class RetrieveImages
    {
        static void Main(string[] args)
        {
            SqlConnection dbCon = new SqlConnection("Server=KOLYOBIQCHA; " +
           "Database=NORTHWND; Integrated Security=true");

            dbCon.Open();

            using (dbCon)
            {
                SqlCommand cmdSql = new SqlCommand("SELECT CategoryName, Picture FROM Categories", dbCon);

                SqlDataReader reader = cmdSql.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        byte[] rawData = (byte[])reader["Picture"];
                        string fileName = reader["CategoryName"].ToString().Replace('/', '_') + ".jpg";
                        int len = rawData.Length;
                        int header = 78;
                        byte[] imgData = new byte[len - header];
                        Array.Copy(rawData, 78, imgData, 0, len - header);

                        MemoryStream memoryStream = new MemoryStream(imgData);
                        System.Drawing.Image image = System.Drawing.Image.FromStream(memoryStream);
                        image.Save(new FileStream(fileName, FileMode.Create), ImageFormat.Jpeg);
                    }
                }
            }
        }
    }
}
