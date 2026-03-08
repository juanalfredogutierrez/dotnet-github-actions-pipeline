using SunatIntegration.Application.DTOs.Sunat;

namespace SunatIntegration.Application.Interfaces
{
    public interface ISunatApiClient
    {
        Task<ExchangeRateDto> GetExchangeRateAsync(DateTime date);
    }
}

