using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CleanArchitecture.WebUI;

public static class HttpContextExtensions
{
    public static string AddFileVersionToPath(this HttpContext context, string path)
    {
        return context
            .RequestServices
            .GetRequiredService<IFileVersionProvider>()
            .AddFileVersionToPath(context.Request.PathBase, path);
    }
}
