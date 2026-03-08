using Microsoft.EntityFrameworkCore;
using SunatIntegration.Domain.Entities;
using SunatIntegration.Domain.Interfaces;
using SunatIntegration.Infrastructure.Persistence;

namespace SunatIntegration.Infrastructure.Repositories
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly AppDbContext _context;

        public ExchangeRateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ExchangeRate?> GetByDateAsync(DateTime date)
        {
            return await _context.ExchangeRates
                .FirstOrDefaultAsync(x => x.DatePublic == date);
        }

        public async Task SaveAsync(ExchangeRate exchangeRate)
        {
            _context.ExchangeRates.Add(exchangeRate);
            await _context.SaveChangesAsync();
        }
    }
}
