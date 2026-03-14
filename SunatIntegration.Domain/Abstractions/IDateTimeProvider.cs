
namespace SunatIntegration.Domain.Abstractions
{
    public interface IDateTimeProvider
    {
        DateTime LocalNow { get; }
    }
}
