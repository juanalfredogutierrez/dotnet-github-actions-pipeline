using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using SunatIntegration.Application.UseCases;
using System.Net;

namespace SunatIntegration.Functions;

public class FunctionConsumerServiceSunat
{
    private readonly ILogger<FunctionConsumerServiceSunat> _logger;
    private readonly SyncExchangeRateUseCase _useCase;

    public FunctionConsumerServiceSunat(ILogger<FunctionConsumerServiceSunat> logger, SyncExchangeRateUseCase useCase)
    {
        _logger = logger;
        _useCase = useCase;
    }

    [Function("query-type-change-sunat")]
    public async Task<HttpResponseData> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
    FunctionContext context)
    {
        await _useCase.ExecuteAsync();
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync($"[{DateTime.Now}]: Tipo de cambio insertado correctamente.");
        return response;
    }

}