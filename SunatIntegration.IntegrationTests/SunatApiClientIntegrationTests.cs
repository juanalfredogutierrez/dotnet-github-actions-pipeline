using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SunatIntegration.Application.UseCases;
using SunatIntegration.Infrastructure.ExternalServices.Sunat;
using SunatIntegration.Infrastructure.Persistence;
using SunatIntegration.Infrastructure.Repositories;

namespace SunatIntegration.IntegrationTests;

public class SunatApiClientIntegrationTests
{
    [Fact]
    public async Task GetExchangeRateAsync_ShouldReturnValidExchangeRate()
    {
        // Arrange
        var httpClient = new HttpClient();

        var client = new SunatApiClient(httpClient);

        // Act
        var result = await client.GetExchangeRateAsync();

        // Assert
        result.Should().NotBeNull();
        result.PriceSales.Should().BeGreaterThan(0);
        result.Pricepurchase.Should().BeGreaterThan(0);
        result.DatePublic.Should().Be(DateTime.Today);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldInsertExchangeRateIntoDatabase()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json") 
            .Build();

        var connectionString = configuration.GetConnectionString("AzureConnection");

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        await using var context = new AppDbContext(options);

        var repository = new ExchangeRateRepository(context);

        var httpClient = new HttpClient();
        var sunatClient = new SunatApiClient(httpClient);

        var useCase = new SyncExchangeRateUseCase(
            sunatClient,
            repository);

        // Act
        await useCase.ExecuteAsync();

        // Assert
        var rate = await context.SunatExchangeRate
            .OrderByDescending(x => x.DatePublic)
            .FirstOrDefaultAsync();

        rate.Should().NotBeNull();
        rate.PriceSales.Should().BeGreaterThan(0);
        rate.Pricepurchase.Should().BeGreaterThan(0);
    }
}