using CQRSMediator.Domain.Entities;
using CQRSMediator.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace CQRSMediator.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerConfiguration());
        }
    }
}
