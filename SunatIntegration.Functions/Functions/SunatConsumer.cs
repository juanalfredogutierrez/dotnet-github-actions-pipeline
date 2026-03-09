using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SunatIntegration.Application.UseCases;

namespace SunatIntegration.Functions.Functions;

public class SunatConsumer
{
    private readonly ILogger<SunatConsumer> _logger;
    private readonly SyncExchangeRateUseCase _useCase;

    public SunatConsumer(ILogger<SunatConsumer> logger, SyncExchangeRateUseCase useCase)
    {
        _logger = logger;
        _useCase = useCase;
    }

    [Function("SunatConsumer")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        await _useCase.ExecuteAsync();
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}