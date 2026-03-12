using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using SunatIntegration.Application.UseCases;
using System.Net;

namespace SunatIntegration.Functions.Functions;

public class FunctionConsumerServiceSunat
{
    private readonly ILogger<FunctionConsumerServiceSunat> _logger;
    private readonly SyncExchangeRateUseCase _useCase;

    public FunctionConsumerServiceSunat(ILogger<FunctionConsumerServiceSunat> logger, SyncExchangeRateUseCase useCase)
    {
        _logger = logger;
        _useCase = useCase;
    }

    [Function(nameof(FunctionConsumerServiceSunat))]
    public async Task<HttpResponseData> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
    FunctionContext context)
    {
        await _useCase.ExecuteAsync();
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync($"Tipo de cambio insertado correctamente.");

        _logger.LogInformation("HTTP trigger ejecutado correctamente.");
        return response;
    }

}