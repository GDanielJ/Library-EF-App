using Library_EF_App.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Library_EF_App.Persistence.EntityConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.Firstname)
                .IsRequired()
                .HasMaxLength(255);

            Property(u => u.Lastname)
                .IsRequired()
                .HasMaxLength(255);

            HasMany(u => u.Orders)
                .WithRequired(o => o.User)
                .HasForeignKey(o => o.UserId);
        }
    }
}
