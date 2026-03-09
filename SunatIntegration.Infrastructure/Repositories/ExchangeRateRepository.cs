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

        public async Task<SunatExchangeRate> GetByDateAsync(DateTime date)
        {
            return await _context.SunatExchangeRate
                .FirstOrDefaultAsync(x => x.DatePublic == date);
        }

        public async Task SaveAsync(SunatExchangeRate exchangeRate)
        {
            _context.SunatExchangeRate.Add(exchangeRate);
            await _context.SaveChangesAsync();
        }
    }
}
