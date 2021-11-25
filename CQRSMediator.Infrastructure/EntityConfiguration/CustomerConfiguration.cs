using CQRSMediator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRSMediator.Infrastructure.EntityConfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(b => b.Name).HasMaxLength(100);
            builder.Property(b => b.Email).HasMaxLength(150);

            builder.HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Jim Morrison",
                    Email = "Jim@email.com"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Elvis Presley",
                    Email = "elvis@email.com"
                },
                new Customer
                {
                    Id = 3,
                    Name = "Janis Joplin",
                    Email = "janis@email.com"
                }
                );
        }
    }
}
