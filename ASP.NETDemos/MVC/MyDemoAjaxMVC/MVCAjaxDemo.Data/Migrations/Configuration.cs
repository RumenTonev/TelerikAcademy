namespace MVCAjaxDemo.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Data.Entity.Validation;
    public sealed class Configuration : DbMigrationsConfiguration<MVCAjaxDemo.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "MVCAjaxDemo.Data.ApplicationDbContext";
        }

        protected override void Seed(MVCAjaxDemo.Data.ApplicationDbContext context)
        {
            context.Actors.Add(
                new Models.LeadingActor
                {
                    Age = 30,
                    Name = "Crazy fucker",
                    StudioAdress = " New York 136",
                    StudioName = "Buffalow"
                });
            context.Actors.Add(
                new Models.LeadingActor
                {
                    Age = 30,
                    Name = " So Crazy fucker",
                    StudioAdress = " New York 137",
                    StudioName = "Buffalow2"
                });
            context.Actors.Add(
                new Models.LeadingActor
                {
                    Age = 30,
                    Name = "So Sop Crazy fucker",
                    StudioAdress = " New York 138",
                    StudioName = "Buffalow3"
                });


            context.Actresess.Add(
                new Models.LeadingActress
                {
                    Age = 30,
                    Name = "Crazya fuckera",
                    StudioAdress = " New York 136",
                    StudioName = "Buffalow"
                });
            context.Actresess.Add(
                new Models.LeadingActress
                {
                    Age = 30,
                    Name = "Crazya fuckera",
                    StudioAdress = " New York 139",
                    StudioName = "Buffalow33"
                });
            context.Actresess.Add(
                new Models.LeadingActress
                {
                    Age = 30,
                    Name = "Crazy fucker",
                    StudioAdress = " New York 136",
                    StudioName = "Buffalow34"
                });
            context.Directors.Add(
                new Models.Director
                {
                    Age = 30,
                    Name = "Crazy ducker",

                });
            context.Directors.Add(
               new Models.Director
               {
                   Age = 35,
                   Name = "Crazy ducker2",

               });
            context.Directors.Add(
               new Models.Director
               {
                   Age = 33,
                   Name = "Crazy ducker3",

               });



            // SUPER VAGNO prihva6ta EF gre6ki!!!!!!

            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

          // context.SaveChanges();



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
        }
    }
}
