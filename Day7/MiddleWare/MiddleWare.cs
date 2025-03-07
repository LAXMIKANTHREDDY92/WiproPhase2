using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspNetCoreMVC.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Middleware 1: Terminate request when URL contains "/end"
            if (context.Request.Path.Value.Contains("/end"))
            {
                await context.Response.WriteAsync("Request terminated at middleware.");
                return; // Stops further execution
            }

            // Middleware 2: Display "Hello1" before moving to next middleware
            await context.Response.WriteAsync("Hello1\n");
            await _next(context); // Call the next middleware

            // Middleware 3: Display "Hello2" after response is processed
            await context.Response.WriteAsync("Hello2\n");
        }
    }
}
