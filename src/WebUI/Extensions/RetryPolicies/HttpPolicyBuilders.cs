using Polly;
using Polly.Extensions.Http;

namespace CleanArchitecture.WebUI.Extensions.RetryPolicies;

public static class HttpPolicyBuilders
{
    public static PolicyBuilder<HttpResponseMessage> GetBaseBuilder()
    {
        return HttpPolicyExtensions
            // Handle HttpRequestExceptions, 408 and 5xx status codes
            .HandleTransientHttpError()
            // Handle 404 not found
            //.OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            // Handle 401 Unauthorized
            //.OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            ;
    }
}
