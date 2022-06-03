using Hangfire.Dashboard;

namespace BackgroundJobUI.Extensions;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();

        // Allow all authenticated users to see the Dashboard (potentially dangerous).
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return httpContext.User.Identity.IsAuthenticated;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}
