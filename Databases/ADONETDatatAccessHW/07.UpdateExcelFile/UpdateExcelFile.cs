//Implement appending new rows to the Excel file.

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07.UpdateExcelFile
{
    class UpdateExcelFile
    {
        static void Main()
        {
            OleDbConnectionStringBuilder csBuilder = new OleDbConnectionStringBuilder();
            csBuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            csBuilder.DataSource = @"..\..\..\Scores.xls";
            csBuilder.Add("Extended Properties", "Excel 12.0 Xml; HDR=YES");

            OleDbConnection connection = new OleDbConnection(csBuilder.ConnectionString);

            connection.Open();

            using (connection)
            {
                OleDbCommand cmd = connection.CreateCommand();

                using (cmd)
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO [main$] (Name, Score) VALUES (@name, @score)";
                    cmd.Parameters.AddWithValue("@name", "AliBaba");
                    cmd.Parameters.AddWithValue("@score", 5);

                    int affected = cmd.ExecuteNonQuery();

                    Console.WriteLine(affected);
                }
            }
        }
    }
}
