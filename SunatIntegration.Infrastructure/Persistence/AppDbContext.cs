using Microsoft.EntityFrameworkCore;
using SunatIntegration.Domain.Abstractions;
using SunatIntegration.Domain.Common;
using SunatIntegration.Domain.Entities;

namespace SunatIntegration.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        public AppDbContext(DbContextOptions<AppDbContext> options, IDateTimeProvider dateTimeProvider)
            : base(options)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public DbSet<SunatExchangeRate> SunatExchangeRate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries<IAuditableEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = _dateTimeProvider.LocalNow;
                    entry.Entity.ModifiedDate = _dateTimeProvider.LocalNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedDate = _dateTimeProvider.LocalNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}