using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SunatIntegration.Application.Interfaces;
using SunatIntegration.Application.Services;
using SunatIntegration.Application.UseCases;
using SunatIntegration.Infrastructure;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();
builder.Services.AddInfraestructura(builder.Configuration);
builder.Services.AddScoped<ICalcularTipoCambioService, CalcularTipoCambioService>();
builder.Services.AddScoped<SyncExchangeRateUseCase>();
builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
