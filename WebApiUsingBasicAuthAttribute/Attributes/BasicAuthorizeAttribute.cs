using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace WebApiUsingBasicAuthAttribute.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key was not provided"
                };
                return;
            }

            var credentialBytes = Convert.FromBase64String(authHeader);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);


            if (credentials[0] != "admin" || credentials[1] != "admin")
            {
                // not logged in - return 401 unauthorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

                // set 'WWW-Authenticate' header to trigger login popup in browsers
                context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
            }
        }
    }
}
