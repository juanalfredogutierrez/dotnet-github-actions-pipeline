using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SunatIntegration.Application.Interfaces;
using SunatIntegration.Application.Services;
using SunatIntegration.Application.UseCases;
using SunatIntegration.Domain.Interfaces;
using SunatIntegration.Infrastructure.ExternalServices.Sunat;
using SunatIntegration.Infrastructure.Persistence;
using SunatIntegration.Infrastructure.Repositories;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
builder.Services.AddScoped<ICalcularTipoCambioService, CalcularTipoCambioService>();
builder.Services.AddScoped<ISunatApiClient, SunatApiClient>();
builder.Services.AddScoped<SyncExchangeRateUseCase>();
builder.Services.AddScoped<HttpClient>();
builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
