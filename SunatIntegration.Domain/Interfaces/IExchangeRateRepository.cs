using SunatIntegration.Domain.Entities;

namespace SunatIntegration.Domain.Interfaces
{
    public interface IExchangeRateRepository
    {
        Task<ExchangeRate?> GetByDateAsync(DateTime date);

        Task SaveAsync(ExchangeRate exchangeRate);
    }
}
