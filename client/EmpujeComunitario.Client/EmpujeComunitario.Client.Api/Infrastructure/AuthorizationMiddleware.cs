using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Api.Infrastructure
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Rutas que NO requieren token (ejemplo)
            var path = context.Request.Path.Value?.ToLower();
            if (path != null && (path.ToLower().Contains("auth/login") || path.Contains("swagger")))
            {
                await _next(context);
                return;
            }

            // Leer el header Authorization
            if (!context.Request.Headers.TryGetValue("Authorization", out var token) || string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var errorResponse = new
                {
                    Message = "Se debe enviar el Token de acceso.",
                    Errors = new[]
                    {
                    new { Field = "Authorization", Message = "Se debe enviar el Token de acceso." }
                }
                };
                await context.Response.WriteAsJsonAsync(errorResponse);
                return;
            }

            // Guardar el token en HttpContext para usarlo después
            context.Items["Authorization"] = token.ToString();

            // Pasar al siguiente middleware
            await _next(context);
        }
    }
}
