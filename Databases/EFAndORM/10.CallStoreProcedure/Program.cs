using NorthwindHW.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10.CallStoreProcedure
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new NORTHWNDEntities())
            {
                var result = context.usp_FindIncomesForPeriod(
                    "Exotic Liquids", 
                    new DateTime(1996, 01, 01), 
                    new DateTime(1997, 12, 12))
                    .First();

                Console.WriteLine(result);
            }
        }
    }
}
