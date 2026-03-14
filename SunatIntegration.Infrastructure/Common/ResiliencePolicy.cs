using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace SunatIntegration.Infrastructure.Common
{
    public static class ResiliencePolicy
    {

        public static AsyncPolicy WrapDbPolicy()
        {
            var retryPolicy = Policy
                .Handle<DbUpdateException>()
                .Or<SqlException>()
                .WaitAndRetryAsync(
                    3,
                    attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    (ex, ts, attempt, ctx) =>
                    {
                        Console.WriteLine($"Retry {attempt} after {ts.TotalSeconds}s due to: {ex.Message}");
                    });

            var circuitBreaker = Policy
                .Handle<DbUpdateException>()
                .Or<SqlException>()
                .CircuitBreakerAsync(
                    2,
                    TimeSpan.FromSeconds(30),
                    onBreak: (ex, ts) => Console.WriteLine($"Circuit broken for {ts.TotalSeconds}s: {ex.Message}"),
                    onReset: () => Console.WriteLine("Circuit reset"),
                    onHalfOpen: () => Console.WriteLine("Circuit half-open")
                );

            return retryPolicy.WrapAsync(circuitBreaker);
        }
    }
}
