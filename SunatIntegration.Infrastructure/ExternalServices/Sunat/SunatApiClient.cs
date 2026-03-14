using SunatIntegration.Application.DTOs.Sunat;
using SunatIntegration.Application.Interfaces;
using System.Globalization;
using System.Net.Http.Json;
namespace SunatIntegration.Infrastructure.ExternalServices.Sunat;

public class SunatApiClient : ISunatApiClient
{
    private const string Endpoint = "listarTipoCambio";

    private readonly HttpClient _httpClient;

    public SunatApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ExchangeRateDto> GetExchangeRateAsync()
    {
        var today = DateTime.Today;

        var payload = new
        {
            anio = today.Year,
            mes = today.Month - 1,
            token = Guid.NewGuid().ToString("N")
        };


        using var response = await _httpClient.PostAsJsonAsync(Endpoint, payload);

        response.EnsureSuccessStatusCode();

        var tipoCambios = await response.Content
            .ReadFromJsonAsync<List<SunatTypeChangeDto>>() ?? [];

        var todayString = today.ToString("dd/MM/yyyy");

        var todayRates = tipoCambios
            .Where(x => x.fecPublica == todayString)
            .ToDictionary(x => x.codTipo, x => x.valTipo);

        todayRates.TryGetValue("V", out var sales);
        todayRates.TryGetValue("C", out var purchase);
        return new ExchangeRateDto
        {
            DatePublic = today,
            PriceSales = ParseDouble(sales),
            Pricepurchase = ParseDouble(purchase)
        };
    }

    private static decimal? ParseDouble(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        return decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
            ? result
            : null;
    }
}