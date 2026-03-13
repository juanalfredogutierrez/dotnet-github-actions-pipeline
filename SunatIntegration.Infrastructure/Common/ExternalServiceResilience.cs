using Polly;

namespace SunatIntegration.Infrastructure.Common
{
    public static class ExternalServiceResilience
    {
        public static AsyncPolicy<HttpResponseMessage> WrapHttpPolicy()
        {
            // Retry con delay exponencial
            var retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(
                    3,
                    attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    (response, ts, attempt, ctx) =>
                    {
                        Console.WriteLine($"Retry {attempt} after {ts.TotalSeconds}s due to HTTP {(int)response.Result.StatusCode}");
                    });

            // Circuit breaker
            var circuitBreaker = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .CircuitBreakerAsync(
                    2,
                    TimeSpan.FromSeconds(30),
                    onBreak: (res, ts) => Console.WriteLine($"Circuit broken for {ts.TotalSeconds}s: HTTP {(int)res.Result.StatusCode}"),
                    onReset: () => Console.WriteLine("Circuit reset"),
                    onHalfOpen: () => Console.WriteLine("Circuit half-open")
                );

            return retryPolicy.WrapAsync(circuitBreaker);
        }
    }
}
