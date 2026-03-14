using SunatIntegration.Domain.Abstractions;

namespace SunatIntegration.Infrastructure.Services
{
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        private static readonly TimeZoneInfo PeruTimeZone =
            TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");

        public DateTime LocalNow =>
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, PeruTimeZone);
    }
}
