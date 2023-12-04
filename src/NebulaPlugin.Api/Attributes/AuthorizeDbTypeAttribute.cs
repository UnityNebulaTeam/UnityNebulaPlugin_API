using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NebulaPlugin.Api.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
public class AuthorizeDbTypeAttribute : TypeFilterAttribute
{
    private readonly string _allowedDbType;

    public AuthorizeDbTypeAttribute(string allowedDbType) : base(typeof(DbAuthorizationFilter))
    {
        Arguments = [allowedDbType];
    }

    // public void OnAuthorization(AuthorizationFilterContext context)
    // {
    //     Console.WriteLine($"----------------> HAS CLAIM: {context.HttpContext.User.HasClaim("dbs", _allowedDbType)}");
    //     Console.WriteLine($"----------------> HAS CLAIM: {context.HttpContext.User.Claims.ToString}");
    //     if (!context.HttpContext.User.HasClaim("dbs", _allowedDbType))
    //         context.Result = new ForbidResult();
    // }


}

public class DbAuthorizationFilter : IAuthorizationFilter
{
    private readonly string _allowedDbType;

    public DbAuthorizationFilter(string allowedDbType) => _allowedDbType = allowedDbType;


    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (!user.Identity.IsAuthenticated)
        {
            context.HttpContext.Response.Body = Stream.Null;
            context.Result = new StatusCodeResult(401);
        }

        var userClaims = ((ClaimsIdentity)user.Identity).Claims;

        var dbsClaim = userClaims.FirstOrDefault(c => c.Type == "dbs");

        bool containsAllowedTypeKey = dbsClaim is not null && dbsClaim.Value.Contains(_allowedDbType);

        if (!containsAllowedTypeKey)
        {
            context.HttpContext.Response.Body = Stream.Null;
            context.Result = new StatusCodeResult(401);
        }
    }
}

