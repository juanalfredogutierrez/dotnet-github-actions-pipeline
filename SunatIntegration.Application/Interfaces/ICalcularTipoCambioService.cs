namespace SunatIntegration.Application.Interfaces
{
    public interface ICalcularTipoCambioService
    {
        Task<double> ObtenerTipoCambioHoy();
    }
}

