namespace ChatApp.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ChatApp.Data;
    internal sealed class Configuration : DbMigrationsConfiguration<ChatApp.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ChatApp.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Roles.AddOrUpdate(
                r => r.Name,
                new IdentityRole { Name = "Administrator" },
                new IdentityRole { Name = "Moderator" },
                new IdentityRole { Name = "Registered user" }
                );
        }
    }
}
