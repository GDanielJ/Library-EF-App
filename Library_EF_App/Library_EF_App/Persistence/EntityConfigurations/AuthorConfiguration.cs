using Library_EF_App.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Library_EF_App.Persistence.EntityConfigurations
{
    public class AuthorConfiguration : EntityTypeConfiguration<Author>
    {
        public AuthorConfiguration()
        {
            Property(a => a.Firstname)
                .IsRequired()
                .HasMaxLength(255);

            Property(a => a.Lastname)
                .IsRequired()
                .HasMaxLength(255);

            HasMany(a => a.Books)
                .WithRequired(b => b.Author)
                .HasForeignKey(b => b.AuthorId);
        }
    }
}
