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
        var rate = await _sunatClient.GetExchangeRateAsync(DateTime.Now);

        var exchangeRate = new ExchangeRate
        {
           // DatePublic = rate.fecPublica,
           //PriceSales = rate.valTipo,
           // Pricepurchase = rate.valTipo
        };

        await _repository.SaveAsync(exchangeRate);
    }
}