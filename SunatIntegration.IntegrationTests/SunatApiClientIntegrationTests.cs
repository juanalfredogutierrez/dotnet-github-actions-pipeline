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
    private readonly HttpClient _httpClient;
    private readonly string _connectionString;
    public SunatApiClientIntegrationTests()
    {
        var configuration = new ConfigurationBuilder()
         .AddJsonFile("appsettings.test.json")
         .Build();
        _connectionString = configuration.GetConnectionString("AzureConnection");

        var baseUrl = configuration["SunatApi:BaseUrl"];
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

    }

    [Fact]
    public async Task GetExchangeRateAsync_ShouldReturnValidExchangeRate()
    {
        // Arrange
        var client = new SunatApiClient(_httpClient);

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
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(_connectionString)
            .Options;

        await using var context = new AppDbContext(options);
        var repository = new ExchangeRateRepository(context);

        var sunatClient = new SunatApiClient(_httpClient);
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