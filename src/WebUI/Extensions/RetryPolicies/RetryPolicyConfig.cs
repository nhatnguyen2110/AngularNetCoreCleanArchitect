namespace CleanArchitecture.WebUI.Extensions.RetryPolicies;

public interface ICircuitBreakerPolicyConfig
{
    int RetryCount { get; set; }
    int BreakDuration { get; set; }
}

public interface IRetryPolicyConfig
{
    int RetryCount { get; set; }
    /// <summary>
    /// pause between retry
    /// </summary>
    int SleepDuration { get; set; }
}

public class RetryPolicyConfig : ICircuitBreakerPolicyConfig, IRetryPolicyConfig
{
    public int RetryCount { get; set; }
    public int SleepDuration { get; set; } //seconds
    public int BreakDuration { get; set; }//seconds
}
