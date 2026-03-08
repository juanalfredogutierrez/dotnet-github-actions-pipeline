using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SunatIntegration.Functions.Functions;

public class SunatConsumer
{
    private readonly ILogger<SunatConsumer> _logger;

    public SunatConsumer(ILogger<SunatConsumer> logger)
    {
        _logger = logger;
    }

    [Function("SunatConsumer")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}