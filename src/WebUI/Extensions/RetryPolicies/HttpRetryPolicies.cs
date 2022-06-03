using Polly;
using Polly.Retry;

namespace CleanArchitecture.WebUI.Extensions.RetryPolicies;

public static class HttpRetryPolicies
{
    public static AsyncRetryPolicy<HttpResponseMessage> GetHttpRetryPolicy(ILogger logger, IRetryPolicyConfig retryPolicyConfig)
    {
        return HttpPolicyBuilders.GetBaseBuilder()
                                      .WaitAndRetryAsync(retryPolicyConfig.RetryCount,
                                                         (retryAttempt) => ComputeDuration(retryPolicyConfig.SleepDuration),
                                                         (result, timeSpan, retryCount, context) =>
                                                         {
                                                             OnHttpRetry(result, timeSpan, retryCount, context, logger);
                                                         });
    }

    private static async void OnHttpRetry(DelegateResult<HttpResponseMessage> result, TimeSpan timeSpan, int retryCount, Polly.Context context, ILogger logger)
    {
        if (result.Result != null)
        {
            try
            {
                var responseText = await result.Result.Content.ReadAsStringAsync().ConfigureAwait(false);
                logger.LogWarning($"Request failed with {(int)result.Result.StatusCode} - {result.Result.StatusCode}. Waiting {timeSpan} before next retry. Retry attempt {retryCount} | Sending {result.Result.RequestMessage?.RequestUri?.Scheme.ToUpper()} request {result.Result.RequestMessage?.Method} {result.Result.RequestMessage?.RequestUri?.AbsoluteUri} | Response: {responseText}");
            }
            catch
            {
                logger.LogWarning($"Request failed with {(int)result.Result.StatusCode} - {result.Result.StatusCode}. Waiting {timeSpan} before next retry. Retry attempt {retryCount} | Sending {result.Result.RequestMessage?.RequestUri?.Scheme.ToUpper()} request {result.Result.RequestMessage?.Method} {result.Result.RequestMessage?.RequestUri?.AbsoluteUri}");
            }
        }
        else
        {
            logger.LogWarning("Request failed because network failure. Waiting {timeSpan} before next retry. Retry attempt {retryCount}", timeSpan, retryCount);
        }
    }

    private static TimeSpan ComputeDuration(int input)
    {
        //return TimeSpan.FromSeconds(Math.Pow(2, input)) + TimeSpan.FromMilliseconds(new Random().Next(0, 100));
        return TimeSpan.FromSeconds(input);
    }
}
