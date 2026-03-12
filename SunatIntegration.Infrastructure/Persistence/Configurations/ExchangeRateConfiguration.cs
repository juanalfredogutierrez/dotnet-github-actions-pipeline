using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SunatIntegration.Domain.Entities;

namespace SunatIntegration.Infrastructure.Persistence.Configurations
{

    public class ExchangeRateConfiguration : IEntityTypeConfiguration<SunatExchangeRate>
    {
        public void Configure(EntityTypeBuilder<SunatExchangeRate> builder)
        {
            builder.ToTable("SunatExchangeRate");

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
