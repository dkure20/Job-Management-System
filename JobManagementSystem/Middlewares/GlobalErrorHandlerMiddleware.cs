using JobManagementSystem.Services.Exceptions.Base;

namespace JobManagementSystem.API.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BaseResponse resp)
            {
                context.Response.StatusCode = resp.codes;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { StatusCode = resp.codes, Message = resp.Message });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { StatusCode = 500, Message = ex.Message });
            }
        }
    }
}
