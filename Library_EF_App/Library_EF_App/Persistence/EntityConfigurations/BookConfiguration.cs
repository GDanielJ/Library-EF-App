﻿using Library_EF_App.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Library_EF_App.Persistence.EntityConfigurations
{
    public class BookConfiguration : EntityTypeConfiguration<Book>
    {
        public BookConfiguration()
        {
            Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(255);

            HasMany<Order>(b => b.Orders)
                .WithMany(o => o.Books)
                .Map(m =>
                {
                    m.MapLeftKey("BookId");
                    m.MapRightKey("OrderId");
                });
        }
    }
}
