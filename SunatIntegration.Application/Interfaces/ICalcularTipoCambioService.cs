namespace SunatIntegration.Application.Interfaces
{
    public interface ICalcularTipoCambioService
    {
        Task<decimal> ObtenerTipoCambioHoy();
    }
}

