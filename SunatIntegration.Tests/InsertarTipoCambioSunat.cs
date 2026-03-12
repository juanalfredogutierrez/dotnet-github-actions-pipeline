using Moq;
using Moq.Protected;
using SunatIntegration.Application.DTOs.Sunat;
using SunatIntegration.Application.Interfaces;
using SunatIntegration.Application.UseCases;
using SunatIntegration.Domain.Entities;
using SunatIntegration.Domain.Interfaces;
using System.Net;
using System.Text;

namespace SunatIntegration.Tests
{
    public class InsertarTipoCambioSunat
    {
        [Fact]

        public async Task InsertTipoCambioAsync_ShouldInsertTodayRate_WhenAvailable()
        {
            var apiMock = new Mock<ISunatApiClient>();
            var repoMock = new Mock<IExchangeRateRepository>();

            apiMock.Setup(x => x.GetExchangeRateAsync())
                .ReturnsAsync(new ExchangeRateDto
                {
                    DatePublic = DateTime.Today,
                    PriceSales = 10,
                    Pricepurchase = 3.70m
                });

            var useCase = new SyncExchangeRateUseCase(
                apiMock.Object,
                repoMock.Object);

            await useCase.ExecuteAsync();

            repoMock.Verify(
                x => x.SaveAsync(It.IsAny<SunatExchangeRate>()),
                Times.Once);
        }

        [Fact]
        public async Task InsertTipoCambioAsync_ShouldFallbackToYesterday_WhenNoTodayRate()
        {
            // Arrange
            var apiMock = new Mock<ISunatApiClient>();
            var repoMock = new Mock<IExchangeRateRepository>();

            var yesterday = DateTime.Today.AddDays(-1);

            apiMock.Setup(x => x.GetExchangeRateAsync())
                .ReturnsAsync(new ExchangeRateDto
                {
                    DatePublic = yesterday,
                    PriceSales = 3.70m,
                    Pricepurchase = 3.65m
                });

            var useCase = new SyncExchangeRateUseCase(
                apiMock.Object,
                repoMock.Object);

            // Act
            await useCase.ExecuteAsync();

            // Assert
            repoMock.Verify(
                x => x.SaveAsync(It.Is<SunatExchangeRate>(r =>
                    r.PriceSales == 3.70m &&
                    r.Pricepurchase == 3.65m)),
                Times.Once);
        }

        [Fact]
        public async Task InsertTipoCambioAsync_ShouldThrowException_WhenApiFails()
        {
            // Arrange
            var apiMock = new Mock<ISunatApiClient>();
            var repoMock = new Mock<IExchangeRateRepository>();

            apiMock.Setup(x => x.GetExchangeRateAsync())
                   .ThrowsAsync(new Exception("Simulated error"));

            var useCase = new SyncExchangeRateUseCase(
                apiMock.Object,
                repoMock.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync());

            Assert.Equal("Simulated error", ex.Message);

            repoMock.Verify(
                x => x.SaveAsync(It.IsAny<SunatExchangeRate>()),
                Times.Never);
        }
    }
}