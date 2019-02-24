namespace Library_EF_App.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Library_EF_App.Core.Domain;

    internal sealed class Configuration : DbMigrationsConfiguration<Library_EF_App.Persistence.LibraryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Library_EF_App.Persistence.LibraryContext context)
        {
            var authors = new List<Author>
            {
                new Author
                {
                    Id = 1,
                    Firstname = "Selma",
                    Lastname = "Lagerlöf"
                },
                new Author
                {
                    Id = 2,
                    Firstname = "Vilhelm",
                    Lastname = "Moberg"
                },
                new Author
                {
                    Id = 3,
                    Firstname = "Hjalmar",
                    Lastname = "Söderberg"
                }
            };

            foreach (var author in authors)
            {
                context.Authors.AddOrUpdate(a => new { a.Firstname, a.Lastname }, author);
            }
        }
    }
}
