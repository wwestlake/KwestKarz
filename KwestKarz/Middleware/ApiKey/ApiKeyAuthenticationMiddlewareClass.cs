using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KwestKarz.Services;
using Microsoft.AspNetCore.Http;

namespace KwestKarz.Middleware.ApiKey
{
    public class ApiKeyAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IApiKeyService apiKeyService)
        {
            var path = context.Request.Path.Value;
            if (path != null && path.StartsWith("/api/gpt"))
            {
                if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader) ||
                    !authHeader.ToString().StartsWith("Bearer "))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Missing or invalid API key");
                    return;
                }

                var token = authHeader.ToString().Substring("Bearer ".Length).Trim();
                var key = await apiKeyService.ValidateKeyAsync(token);
                if (key == null)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Invalid or inactive API key");
                    return;
                }

                var claims = key.Claims.Select(c => new Claim(ClaimTypes.Role, c.Role.ToString()));
                var identity = new ClaimsIdentity(claims, "ApiKey");
                var principal = new ClaimsPrincipal(identity);
                context.User = principal;
            }

            await _next(context);
        }
    }
}
