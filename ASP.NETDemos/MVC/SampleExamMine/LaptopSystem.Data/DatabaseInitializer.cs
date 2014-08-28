using LaptopSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopSystem.Data
{
    public class DatabaseInitializer : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public DatabaseInitializer()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if(context.Roles.Count()==0)
            {
                var role=new IdentityRole("Admin");
                context.Roles.Add(role);
                context.SaveChanges();
                var usert = context.Users.FirstOrDefault(x => x.Email == "admin@yahoo.com");
                var userRole = new IdentityUserRole();
                userRole.RoleId = role.Id;
                userRole.UserId = usert.Id;
                role.Users.Add(userRole);
                
                context.SaveChanges();
                //var drole = context.Roles.FirstOrDefault(x => x.Name == "Admin");
                
               // username.Roles.Add(drole);
                //context.SaveChanges();
               ApplicationUser admin = new ApplicationUser() { UserName = "admin", Email = "admin@admin.com",  };
            }
            
            if (context.Laptops.Count() > 0)
            {
                return;
            }

            Random rand = new Random();

            Manufacturer sampleManufacturer = new Manufacturer { Name = "Lenovo" };
            ApplicationUser user = new ApplicationUser() { UserName = "TestUser", Email = "TestMail@test.com" };

            for (int i = 0; i < 10; i++)
            {
                Laptop laptop = new Laptop();
                laptop.HardDiskSize = rand.Next(10, 1000);
                laptop.ImageUrl = "http://laptop.bg/system/images/26207/thumb/toshiba_satellite_l8501v8.jpg";
                laptop.Manufacturer = sampleManufacturer;
                laptop.Model = "ideapad";
                laptop.MonitorSize = 15.4;
                laptop.Price = rand.Next(600, 3000);
                laptop.RamMemorySize = rand.Next(1, 16);
                laptop.Weight = 3;

                var votesCount = rand.Next(0, 10);
                for (int j = 0; j < votesCount; j++)
                {
                    laptop.Votes.Add(new Vote { Laptop = laptop, VotedBy = user });
                }

                var commentsCount = rand.Next(0, 10);
                for (int j = 0; j < commentsCount; j++)
                {
                    laptop.Comments.Add(new Comment { Content = "Mnou qk laptop brat.", Author = user });
                }

                context.Laptops.Add(laptop);
            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
