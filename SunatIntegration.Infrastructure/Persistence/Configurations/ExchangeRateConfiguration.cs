using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SunatIntegration.Domain.Entities;

namespace SunatIntegration.Infrastructure.Persistence.Configurations
{

    public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
    {
        public void Configure(EntityTypeBuilder<ExchangeRate> builder)
        {
            builder.ToTable("ExchangeRates");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DatePublic)
                .IsRequired();

            builder.HasKey(e => e.Id);

            builder.Property(e => e.DatePublic)
                .IsRequired();

            builder.Property(e => e.PriceSales)
                .HasPrecision(10, 3);

            builder.Property(e => e.Pricepurchase)
                .HasPrecision(10, 3);
        }
    }
}
