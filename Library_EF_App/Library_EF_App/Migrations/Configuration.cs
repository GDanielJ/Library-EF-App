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
            #region Add Authors
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
                context.Authors.AddOrUpdate(a => new { a.Firstname, a.Lastname }, author);
            #endregion

            #region Add Books
            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Name = "Jerusalem",
                    Author = authors[0]
                },
                new Book
                {
                    Id = 2,
                    Name = "Kejsaren av Portugallien",
                    Author = authors[0]
                },
                new Book
                {
                    Id = 3,
                    Name = "Utvandrarna",
                    Author = authors[1]
                },
                new Book
                {
                    Id = 4,
                    Name = "Rid i natt!",
                    Author = authors[1]
                },
                new Book
                {
                    Id = 5,
                    Name = "Doktor Glas",
                    Author = authors[2]
                }
            };

            foreach (var book in books)
                context.Books.AddOrUpdate(b => b.Name, book);
            #endregion

            #region Add Users
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Firstname = "Kalle",
                    Lastname = "Karlsson"
                },
                new User
                {
                    Id = 2,
                    Firstname = "Pelle",
                    Lastname = "Persson"
                }
            };

            foreach (var user in users)
                context.Users.AddOrUpdate(u => new { u.Firstname, u.Lastname}, user);
            #endregion

            #region Add Orders
            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    CheckOutDate = new DateTime(2019, 2, 20),
                    ReturnDate = new DateTime(2019, 3, 18),
                    User = users[0]
                },
                new Order
                {
                    Id = 2,
                    CheckOutDate = new DateTime(2019, 1, 30),
                    ReturnDate = new DateTime(2019, 2, 28),
                    User = users[1]
                }
            };

            foreach (var order in orders)
                context.Orders.AddOrUpdate(o => o.Id, order);
            #endregion
        }
    }
}
