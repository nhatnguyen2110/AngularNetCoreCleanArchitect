using CleanArchitecture.Domain;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Infrastructure.Identity;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    public CustomAuthorizeAttribute() : base()
    {
        AuthenticationSchemes = Constants.CustomAuthenticationScheme;
    }
    public CustomAuthorizeAttribute(string policy) : base(policy)
    {
        AuthenticationSchemes = Constants.CustomAuthenticationScheme;
    }
}