//task 6

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06.ReadFromExcel
{
    class ReadFromExcel
    {
        static void Main()
        {
            OleDbConnectionStringBuilder csbuilder = new OleDbConnectionStringBuilder();
            csbuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            csbuilder.DataSource = @"..\..\..\Scores.xls";
            csbuilder.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES");

            OleDbConnection oleDbCon = new OleDbConnection(csbuilder.ConnectionString);

            oleDbCon.Open();

            using (oleDbCon)
            {
                OleDbCommand cmd = new OleDbCommand("SELECT Name, Score FROM [main$]", oleDbCon);

                OleDbDataReader reader = cmd.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} - {1}", reader["Name"], reader["Score"]);
                    }
                }
            }
        }
    }
}
