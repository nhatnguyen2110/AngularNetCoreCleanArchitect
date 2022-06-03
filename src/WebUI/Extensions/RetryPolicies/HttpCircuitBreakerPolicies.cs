using Polly;
using Polly.CircuitBreaker;

namespace CleanArchitecture.WebUI.Extensions.RetryPolicies;

public class HttpCircuitBreakerPolicies
{
    public static AsyncCircuitBreakerPolicy<HttpResponseMessage> GetHttpCircuitBreakerPolicy(ILogger logger, ICircuitBreakerPolicyConfig circuitBreakerPolicyConfig)
    {
        return HttpPolicyBuilders.GetBaseBuilder()
                                  .CircuitBreakerAsync(circuitBreakerPolicyConfig.RetryCount + 1,
                                                       TimeSpan.FromSeconds(circuitBreakerPolicyConfig.BreakDuration),
                                                       (result, breakDuration) =>
                                                       {
                                                           OnHttpBreak(result, breakDuration, circuitBreakerPolicyConfig.RetryCount, logger);
                                                       },
                                                       () =>
                                                       {
                                                           OnHttpReset(logger);
                                                       });
    }

    public static async void OnHttpBreak(DelegateResult<HttpResponseMessage> result, TimeSpan breakDuration, int retryCount, ILogger logger)
    {
        if (result.Result != null)
        {
            try
            {
                var responseText = await result.Result.Content.ReadAsStringAsync().ConfigureAwait(false);
                logger.LogWarning($"Service shutdown during {breakDuration} after {retryCount} failed retries. | Sending {result.Result.RequestMessage?.RequestUri?.Scheme.ToUpper()} request {result.Result.RequestMessage?.Method} {result.Result.RequestMessage?.RequestUri?.AbsoluteUri} | Response: {responseText}");
            }
            catch
            {
                logger.LogWarning("Service shutdown during {breakDuration} after {DefaultRetryCount} failed retries.", breakDuration, retryCount);
            }
        }
        else
        {
            logger.LogWarning("Service shutdown during {breakDuration} after {DefaultRetryCount} failed retries.", breakDuration, retryCount);
        }
        throw new BrokenCircuitException("Service inoperative. Please try again later");
    }

    public static void OnHttpReset(ILogger logger)
    {
        logger.LogInformation("Service restarted.");
    }
}
