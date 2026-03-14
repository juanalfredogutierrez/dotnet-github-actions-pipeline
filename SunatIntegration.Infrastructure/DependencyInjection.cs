using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SunatIntegration.Application.Interfaces;
using SunatIntegration.Domain.Abstractions;
using SunatIntegration.Domain.Interfaces;
using SunatIntegration.Infrastructure.Common;
using SunatIntegration.Infrastructure.ExternalServices.Sunat;
using SunatIntegration.Infrastructure.Persistence;
using SunatIntegration.Infrastructure.Repositories;
using SunatIntegration.Infrastructure.Services;

namespace SunatIntegration.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructura(this IServiceCollection services, IConfiguration configuration)
        {
            //base de datos 
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("AzureConnection")));

            //repositorios
            services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();

            // servicios de dominio
            services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

            //servicios externos
            services.AddHttpClient<ISunatApiClient, SunatApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["SunatApi:BaseUrl"]);
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            })
            .AddPolicyHandler(ExternalServiceResilience.GetRetryPolicy())
            .AddPolicyHandler(ExternalServiceResilience.GetCircuitBreakerPolicy());


            return services;
        }
    }
}
