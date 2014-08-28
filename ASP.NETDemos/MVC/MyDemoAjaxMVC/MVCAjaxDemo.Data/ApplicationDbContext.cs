using Microsoft.AspNet.Identity.EntityFramework;
using MVCAjaxDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAjaxDemo.Data
{
   
        public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Director> Directors { get; set; }

        public DbSet<LeadingActor> Actors { get; set; }

        public DbSet<LeadingActress> Actresess { get; set; }

        public DbSet<Movie> Movies { get; set; }
           

    }


    
}
