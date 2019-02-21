using System.Data.Entity;
using Library_EF_App.Core.Domain;
using Library_EF_App.Persistence.EntityConfigurations;

namespace Library_EF_App.Persistence
{
    public class LibraryContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }

        public LibraryContext()
            : base("name=LibraryContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookConfiguration());
            modelBuilder.Configurations.Add(new AuthorConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
        }
    }
}
