using SunatIntegration.Application.Interfaces;
using SunatIntegration.Domain.Entities;
using SunatIntegration.Domain.Interfaces;

namespace SunatIntegration.Application.UseCases;

public class SyncExchangeRateUseCase
{
    private readonly ISunatApiClient _sunatClient;
    private readonly IExchangeRateRepository _repository;

    public SyncExchangeRateUseCase(
        ISunatApiClient sunatClient,
        IExchangeRateRepository repository)
    {
        _sunatClient = sunatClient;
        _repository = repository;
    }

    public async Task ExecuteAsync()
    {
        var resultTypeChange = await _sunatClient.GetExchangeRateAsync();

        var exchangeRate = new ExchangeRate
        {
            DatePublic = resultTypeChange.DatePublic.Value,
            PriceSales = resultTypeChange.PriceSales,
            Pricepurchase = resultTypeChange.Pricepurchase
        };

        await _repository.SaveAsync(exchangeRate);
    }
}