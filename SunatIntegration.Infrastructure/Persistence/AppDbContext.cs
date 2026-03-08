using Microsoft.EntityFrameworkCore;
using SunatIntegration.Domain.Entities;

namespace SunatIntegration.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExchangeRate>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.DatePublic)
                    .IsRequired();

                entity.Property(e => e.PriceSales)
                    .HasPrecision(10, 3);

                entity.Property(e => e.Pricepurchase)
                    .HasPrecision(10, 3);
            });
        }
    }
}