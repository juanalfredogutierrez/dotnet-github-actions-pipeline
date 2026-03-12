using SunatIntegration.Application.Interfaces;
using SunatIntegration.Domain.Interfaces;

namespace SunatIntegration.Application.Services
{
    public class CalcularTipoCambioService : ICalcularTipoCambioService
    {
        private readonly IExchangeRateRepository _repository;

        public CalcularTipoCambioService(IExchangeRateRepository repository)
        {
            _repository = repository;
        }

        public async Task<decimal> ObtenerTipoCambioHoy()
        {
            var tipoCambio = await _repository.GetByDateAsync(DateTime.Today);

            if (tipoCambio == null)
                throw new Exception("No existe tipo de cambio para hoy");

            return tipoCambio.PriceSales.Value;
        }
    }
}
