using MVCBugTracking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCBugTracking.Data
{
    public class DataContext:DbContext
    {

        public IDbSet<Bug> Bugs { get; set; }
    }
}
