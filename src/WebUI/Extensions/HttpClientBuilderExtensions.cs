﻿using CleanArchitecture.WebUI.Extensions.RetryPolicies;

namespace CleanArchitecture.WebUI.Extensions;

public static class HttpClientBuilderExtensions
{
    public static IHttpClientBuilder AddPolicyHandlers(this IHttpClientBuilder httpClientBuilder, string policySectionName, ILoggerFactory loggerFactory, IConfiguration configuration)
    {
        var retryLogger = loggerFactory.CreateLogger("PollyHttpRetryPoliciesLogger");
        var circuitBreakerLogger = loggerFactory.CreateLogger("PollyHttpCircuitBreakerPoliciesLogger");

        var policyConfig = new RetryPolicyConfig();
        configuration.Bind(policySectionName, policyConfig);

        var circuitBreakerPolicyConfig = (ICircuitBreakerPolicyConfig)policyConfig;
        var retryPolicyConfig = (IRetryPolicyConfig)policyConfig;

        return httpClientBuilder
            .AddRetryPolicyHandler(retryLogger, retryPolicyConfig)
            //.AddCircuitBreakerHandler(circuitBreakerLogger, circuitBreakerPolicyConfig)
            ;
    }

    public static IHttpClientBuilder AddRetryPolicyHandler(this IHttpClientBuilder httpClientBuilder, ILogger logger, IRetryPolicyConfig retryPolicyConfig)
    {
        return httpClientBuilder.AddPolicyHandler(HttpRetryPolicies.GetHttpRetryPolicy(logger, retryPolicyConfig));
    }

    public static IHttpClientBuilder AddCircuitBreakerHandler(this IHttpClientBuilder httpClientBuilder, ILogger logger, ICircuitBreakerPolicyConfig circuitBreakerPolicyConfig)
    {
        return httpClientBuilder.AddPolicyHandler(HttpCircuitBreakerPolicies.GetHttpCircuitBreakerPolicy(logger, circuitBreakerPolicyConfig));
    }
}
