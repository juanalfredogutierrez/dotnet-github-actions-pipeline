using SunatIntegration.Domain.Entities;

namespace SunatIntegration.Domain.Interfaces
{
    public interface IExchangeRateRepository
    {
        Task<SunatExchangeRate> GetByDateAsync(DateTime date);

        Task SaveAsync(SunatExchangeRate exchangeRate);
    }
}
